using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using DT.PCP.BussinesServices;
using DT.PCP.BussinesServices.Impl;
using DT.PCP.CommonDomain;
using DT.PCP.Domain;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Utils;
using DT.PCP.Utils.Impl;
using DT.PCP.Web.Core;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.ViewModels.Cabinet;
using Lib.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace DT.PCP.Web.Portal.Controllers
{
    [EnhancedAuthorize]
    public class CabinetController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IViewModelCreator _viewModelCreator;
        private readonly IEntityCreator _entityCreator;
        private readonly IViolationService _violationService;
        private readonly INotificationService _notificationService;
        private readonly INotificationSubscriptionService<EmailNotificationSettings, EmailNotification> _emailSubscriptionService;
        private readonly INotificationSubscriptionService<SmsNotificationSettings, SmsNotification> _smsSubscribeService;

        public CabinetController(IUserService userService, IViewModelCreator viewModelCreator, IEntityCreator entityCreator, IViolationService violationService, INotificationService notificationService, INotificationSubscriptionService<EmailNotificationSettings, EmailNotification> emailSubscriptionService, INotificationSubscriptionService<SmsNotificationSettings, SmsNotification> smsSubscribeService)
        {
            _userService = userService;
            _viewModelCreator = viewModelCreator;
            _entityCreator = entityCreator;
            _violationService = violationService;
            _notificationService = notificationService;
            _emailSubscriptionService = emailSubscriptionService;
            _smsSubscribeService = smsSubscribeService;
        }

        [AllowAnonymous]
        public ActionResult AuthRequired()
        {
            return PartialView();
        }


        public ActionResult ViolationList(bool? payed)
        {
            var model = new ViolationListViewModel
                {
                    ShowPayed = true,
                    CarNumber = PcpSession.CurrentUser.CarNumber,
                    CarMark = PcpSession.CurrentUser.CarMark,
                    CarColor = PcpSession.CurrentUser.CarColor,
                    Violations = GetViol(payed)
                };
            ViewData["Payed"] = payed;
            if (Request.IsAjaxRequest())
                return PartialView("_Violation", model.Violations);

            return View(model);
        }

        //public ActionResult ViolationPayed()
        //{
        //    var model = GetViol(true);
        //    return PartialView("_Violation", model);
        //}

        private IEnumerable<CarViolationViewModel> GetViol(bool? payed)
        {
            var violationResults = _violationService.GetViolations(PcpSession.CurrentUser.CarNumber, PcpSession.CurrentUser.CarPassportNumber);
            IEnumerable<TrafficViolationData> filteredViolations;
            if (violationResults == null)
                return new List<CarViolationViewModel>().OrderBy(c => c.Cost);
            if (payed == null || !payed.Value)
            {
                filteredViolations =
                    violationResults.TrafficViolationDataList.Where(
                        v =>
                        v.ViolationStatusEnum == ViolationStatusEnum.OrderCreated ||
                        v.ViolationStatusEnum == ViolationStatusEnum.OrderPrinted ||
                        v.ViolationStatusEnum == ViolationStatusEnum.NotifySended).ToList();
            }
            else
            {
                filteredViolations =
                   violationResults.TrafficViolationDataList.Where(
                       v =>
                       v.ViolationStatusEnum == ViolationStatusEnum.Payed ||
                       v.ViolationStatusEnum == ViolationStatusEnum.PayChecked ||
                       v.ViolationStatusEnum == ViolationStatusEnum.Canceled).ToList();

            }
            var model = _viewModelCreator.Create<CarViolationViewModel, TrafficViolationData>(
                filteredViolations.OrderBy(c => c.FixationDateTime).ToList());
            return model;
        }

        public ActionResult ViolationListData()
        {
            var model = GetViol(true);
            return PartialView("_Violation", model);
        }


        public ActionResult Info()
        {
            var info = PcpSession.CurrentUser;
            var model = _viewModelCreator.Create<UserInfoViewModel, User>(info);
            return View(model);
        }

        public ActionResult Details(string orderNumber)
        {
            var violationResult = _violationService.GetViolationsByOrder(orderNumber, PcpSession.CurrentUser.CarNumber);
           
            var model = _viewModelCreator.Create<CarViolationDetailsViewModel, TrafficViolationData>(violationResult);
            model.StatusImageName = GetImageFileNameForStatus(model.StatusEnum);
            return PartialView("Detail", model);
        }

        private string GetImageFileNameForStatus(ViolationStatusEnum status)
        {
            if(status == ViolationStatusEnum.Payed || status == ViolationStatusEnum.PayChecked)
                return "payed.png";

            if(status == ViolationStatusEnum.OrderPrinted || status == ViolationStatusEnum.OrderCreated)
                return "not-pay.png";

            if(status == ViolationStatusEnum.Canceled)
                return "closed.png";

            if(status == ViolationStatusEnum.NotifySended)
                return "transfer.png";

            return string.Empty;
        }




        [AcceptVerbs(HttpVerbs.Post)]
        [NoCache]
        public ActionResult Violation(JqGridRequest request, bool? showNotPayed)
        {

            var violationResults = _violationService.GetViolations(PcpSession.CurrentUser.CarNumber, PcpSession.CurrentUser.CarPassportNumber);
            IEnumerable<TrafficViolationData> filteredViolations;

            if (showNotPayed != null && showNotPayed.Value)
            {
                filteredViolations =
                    violationResults.TrafficViolationDataList.Where(
                        v =>
                        v.ViolationStatusEnum == ViolationStatusEnum.OrderCreated ||
                        v.ViolationStatusEnum == ViolationStatusEnum.OrderPrinted ||
                        v.ViolationStatusEnum == ViolationStatusEnum.NotifySended).ToList();
            }
            else
            {
                filteredViolations =
                   violationResults.TrafficViolationDataList.Where(
                       v =>
                       v.ViolationStatusEnum == ViolationStatusEnum.Payed ||
                       v.ViolationStatusEnum == ViolationStatusEnum.PayChecked ||
                       v.ViolationStatusEnum == ViolationStatusEnum.Canceled).ToList();

            }
            var model = _viewModelCreator.Create<CarViolationViewModel, TrafficViolationData>(
                filteredViolations).ToList().OrderBy(c => c.ViolationType);

            JqGridResponse response = new JqGridResponse()
                 {
                     TotalRecordsCount = model.Count(),
                     UserData = new
                     {
                         FixationTime = "Итого нарушений: ",
                         OrderNumber = model.Count(),
                         Status = "Сумма:",
                         Cost = model.Sum(c => double.Parse(c.Cost))
                     }
                 };

            response.Records.AddRange(from violation in model
                                      select
                                          new JqGridRecord<CarViolationViewModel>(violation.OrderNumber, violation)
                );


            return new JqGridJsonResult() { Data = response };
        }


        public ActionResult Edit()
        {
            var model = _viewModelCreator.Create<UserInfoViewModel, User>(PcpSession.CurrentUser);

            if (PcpSession.CurrentUser.IsArtificialPerson)
                return PartialView("EditArtificial", model);

            return PartialView(model);
        }

        public ActionResult ConfirmationNotification()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ConfirmationNotification(ConfirmationNotificationViewModel model)
        {
            var isValid = _userService.CheckNotificationCode(PcpSession.CurrentUser.CarNumber, PcpSession.CurrentUser.CarPassportNumber, model.Code);

            if (!isValid)
                ModelState.AddModelError("Code", "Код не верен");

            if (!ModelState.IsValid)
            {
                string view = this.RenderPartialView("ConfirmationNotification", model);
                return new JsonResult
                    {
                        Data = new
                            {
                                success = false,
                                view = view
                            }
                    };
            }

            PcpSession.CurrentUser = _notificationService.ConfirmNotification(PcpSession.CurrentUser, model.Code);

            return new JsonResult
            {
                Data = new
                {
                    success = true,

                }
            };


        }

        [HttpPost]
        public ActionResult Edit(UserInfoViewModel userInfo)
        {
            if (!ModelState.IsValid)
            {
                string view = this.RenderPartialView(PcpSession.CurrentUser.IsArtificialPerson ? "EditArtificial" : "Edit", userInfo);
                return new JsonResult
                    {
                        Data = new
                            {
                                success = false,
                                view = view
                            }
                    };
            }


            var user = _entityCreator.Create<User, UserInfoViewModel>(userInfo);
            //******
            var checkCode = Helpers.GetCode();
            var updateEmailResult = _emailSubscriptionService.SubscribeOrUnsubscribe(PcpSession.CurrentUser, user, checkCode);
            var updateSmsResult = _smsSubscribeService.SubscribeOrUnsubscribe(PcpSession.CurrentUser, user, checkCode);
            var updateResult = new UpdateNotificationResult
                {
                    ShowConfirmation = updateEmailResult.ShowConfirmation || updateSmsResult.ShowConfirmation,
                    User = updateSmsResult.User
                };

            //****** 





            //var updateResult = _notificationService.UpdateNotification(PcpSession.CurrentUser, user);
            PcpSession.CurrentUser = updateResult.User;


            //TODO рефакторинг (написать нормальный маппер)
            userInfo.CarNumber = user.CarNumber = PcpSession.CurrentUser.CarNumber;
            userInfo.CarPassportNumber = user.CarPassportNumber = PcpSession.CurrentUser.CarPassportNumber;
            user.IsArtificialPerson = PcpSession.CurrentUser.IsArtificialPerson;
            user.TaxNumber = PcpSession.CurrentUser.TaxNumber;

            _userService.UpdateUser(user);
            PcpSession.CurrentUser = _userService.GetUser(user.CarNumber, user.CarPassportNumber);
            string confirmationNotificationView = this.RenderPartialView("ConfirmationNotification");
            return new JsonResult
            {
                Data = new
                {
                    success = true,
                    view = this.RenderPartialView("InfoPartial", userInfo),
                    showNotificationCheck = updateResult.ShowConfirmation,
                    confirmationNotificationView = confirmationNotificationView
                }
            };
        }

        public ActionResult Notification()
        {
            var emailNotification = PcpSession.CurrentUser.Notifications.OfType<EmailNotification>().FirstOrDefault();
            var smsNotification = PcpSession.CurrentUser.Notifications.OfType<SmsNotification>().FirstOrDefault();
            var model = new NotificationViewModel()
            {
                Email = PcpSession.CurrentUser.Email,
                Phone = PcpSession.CurrentUser.PhoneNumber,
                EmailNotification = emailNotification != null && emailNotification.IsConfirmed,
                SmsNotification = smsNotification != null && smsNotification.IsConfirmed,
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Notification(NotificationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                string view = this.RenderPartialView("Notification", model);
                return new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        view = view
                    }
                };
            }

            var setting = _entityCreator.Create<NotificationSetting, NotificationViewModel>(model);
            ////*******

            var checkCode = Helpers.GetCode();
            var emailNotificationSetting = new EmailNotificationSettings()
                {
                    Email = setting.Email,
                    EmailNotification = setting.EmailNotification,
                    Code = checkCode
                };

            var emailNotification = PcpSession.CurrentUser.Notifications.OfType<EmailNotification>().FirstOrDefault();
            var updateEmailResult = _emailSubscriptionService.SubscribeOrUnsubscribe(PcpSession.CurrentUser, emailNotificationSetting, emailNotification);

            var smsNotificationSetting = new SmsNotificationSettings()
                {
                    Phone = setting.Phone,
                    SmsNotification = setting.SmsNotification,
                    Code = checkCode
                };

            var smsNotification = PcpSession.CurrentUser.Notifications.OfType<SmsNotification>().FirstOrDefault();
            var updateSmsResult = _smsSubscribeService.SubscribeOrUnsubscribe(PcpSession.CurrentUser, smsNotificationSetting, smsNotification);

            var updateResult = new UpdateNotificationResult
                {
                    ShowConfirmation = updateEmailResult.ShowConfirmation || updateSmsResult.ShowConfirmation,
                    User = updateSmsResult.User
                };

            //*******
            //var updateResult = _notificationService.UpdateNotification(PcpSession.CurrentUser, setting);
            PcpSession.CurrentUser = updateResult.User;

            var userInfoModel = _viewModelCreator.Create<UserInfoViewModel, User>(PcpSession.CurrentUser);

            return new JsonResult
            {
                Data = new
                {
                    success = true,
                    view = this.RenderPartialView("InfoPartial", userInfoModel),
                    showNotificationCheck = updateResult.ShowConfirmation
                }
            };

        }

        public ActionResult Unsubscribe(string email, string hash)
        {
            var result = _userService.UnsubscribeNotification(email, hash);
            if (result)
                return View();

            return View("UnsubscribeError");
        }

        public ActionResult LoadFromService()
        {
            var user = _userService.GetUserFromService(PcpSession.CurrentUser.CarNumber, PcpSession.CurrentUser.CarPassportNumber, PcpSession.CurrentUser.Id);
            if (user == null)
                return PartialView("UserInfoNotFound");

            var model = _viewModelCreator.Create<UserInfoViewModel, User>(user);
            model.Email = PcpSession.CurrentUser.Email;
            model.PhoneNumber = PcpSession.CurrentUser.PhoneNumber;
            if (PcpSession.CurrentUser.IsArtificialPerson)
                return PartialView("EditArtificial", model);

            return PartialView("Edit", model);
        }

        public ActionResult GetViolationImage(string orderNumber)
        {
            //TODO ввести абстракция для сохранения файлов в sorage или на диске
            var image = _violationService.GetViolationImagePath(orderNumber);
            //FileStream fileStream = new FileStream(image, FileMode.Open, FileAccess.Read);
            //byte[] buffer = new byte[fileStream.Length];
            //fileStream.Read(buffer, 0, (int)fileStream.Length);
            //fileStream.Close();
            //string str = System.Convert.ToBase64String(buffer, 0, buffer.Length);
            return Json(new { Image = image }, JsonRequestBehavior.AllowGet);
        }
    }
}