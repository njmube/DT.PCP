using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using DT.PCP.BussinesServices;
using DT.PCP.DataAccess;
using DT.PCP.Domain;
using DT.PCP.Epay;
using DT.PCP.Logging;
using DT.PCP.Utils;
using DT.PCP.Web.Core;
using DT.PCP.Web.ViewModels.Pay;

namespace DT.PCP.Web.Portal.Controllers
{
   
    public class PayController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IViewModelCreator _viewModelCreator;
        private readonly IEpayService _epayService;
        private readonly IViolationService _violationService;
        private readonly IEmailService _emailService;
        private readonly IRepository _repository;
        private readonly IOsmpPaymentService _osmpPaymentService;

        public PayController(ILogger logger, IUserService userService, IOrderService orderService, IViewModelCreator viewModelCreator, IEpayService epayService, IViolationService violationService, IEmailService emailService, IRepository repository, IOsmpPaymentService osmpPaymentService)
        {
            _logger = logger;
            _userService = userService;
            _orderService = orderService;
            _viewModelCreator = viewModelCreator;
            _epayService = epayService;
            _violationService = violationService;
            _emailService = emailService;
            _repository = repository;
            _osmpPaymentService = osmpPaymentService;
        }

        [HttpPost, ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult EpayError()
        {
            string response = Request.Params["response"];
            _logger.Debug(response);
            return Content("0");
        }


        [HttpPost]
        [ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult EpayResponse()
        {
            try
            {
                string response = Request.Params["response"];
                _logger.Debug(response);
                var isFixed = _epayService.FixPayment(response);

                //TODO рефакторинг
                var ePayResponse = _epayService.ParseResponse(response);
                var order = _repository.Query<Order>().FirstOrDefault(o => o.EpayOrderId == ePayResponse.EpayOrderId);
                foreach (var orderDetail in order.Details)
                {
                    var violation = _violationService.GetViolationsByOrder(orderDetail.OrderNumber, order.User.CarNumber);
                    var model = new VaucherViewModel
                        {
                            CarNumber = violation.TransportNumber,
                            CarPassportNumber = violation.NumberSRTS,
                            Color = violation.Color,
                            Cost = (decimal) violation.FineCost,
                            FixationDateTime = violation.FixationDateTime,
                            Mark = violation.Mark,
                            OrderNumber = violation.OrderNumber,
                            PostAddress = violation.PostAddress,
                            ViolationType = violation.ViolationType,
                            IsArtificialPerson = violation.IsLegalEntity,
                            PaymentDateTime = order.PaymentDate.Value,
                            FullName = violation.ViolatorFullName
                         
                        };
                    var htmlBody = this.RenderPartialView("Vaucher", model);
                    _emailService.SendEmail(order.User.Email, "Оплата нарушения", htmlBody);
                }
            }
            catch (Exception e)
            {
                _logger.Debug( e.Message);
                _logger.Debug( e.StackTrace);
            }
            return Content("0");
        }

        public ActionResult Test()
        {
            var text = "<document><bank name=\"Kazkommertsbank JSC\"><customer name=\"MAXIM\" mail=\"sharok@mail.ru\" phone=\"\"><merchant cert_id=\"00C182B189\" name=\"Test shop\"><order order_id=\"10000006\" amount=\"20.00\" currency=\"398\"><department merchant_id=\"92061101\" amount=\"20.00\"/></order></merchant><merchant_sign type=\"RSA\"/></customer><customer_sign type=\"RSA\"/><results timestamp=\"2012-08-27 10:44:48\"><payment merchant_id=\"92061101\" card=\"440564-XX-XXXX-6150\" amount=\"20.00\" reference=\"120827104448\" approval_code=\"104448\" response_code=\"00\" Secure=\"Yes\" card_bin=\"KAZ\"/></results></bank><bank_sign cert_id=\"00C18327E8\" type=\"SHA/RSA\">c17J7hOklboKJYZXqLYj/zfFMMYFiW+5GASGD1YhZJq8yHsgXdJANQWEfqqucRV46D4btX2XZCmCFNIEMgzAqQOGXUc/kSgK3mTZViYwzZn7qUE0JUmN0bylNz9vN18aWgGnhzEepTjdW8oNdsR+BG/jaO7mXRcr3h5iWTQyAnM=</bank_sign></document>";
            

            
            _epayService.FixPayment(text);
            return new EmptyResult();
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ConfirmOrder()
        {
            var commision = _epayService.GetCommission();
            var totalPrice = _orderService.CalculateCommission(PcpSession.Order, commision);

            _orderService.CreateOrder(PcpSession.Order);

            var price = _orderService.CalculateCost(PcpSession.Order);
            
            var priceWithCommission = _orderService.CalculateCommission(price, commision);
            
            var model = new ConfirmOrderViewModel
                {
                    Order = PcpSession.Order,
                    CommissionPercent = commision,
                    TotalPrice = totalPrice,
                    SignedString = EPayKkb.Build64(PcpSession.Order.EpayOrderId, priceWithCommission),
                    Appendix = BuildAppendixField(PcpSession.Order, commision),
                    Email = PcpSession.CurrentUser.Email
                };

            return View(model);
        }



        public ActionResult PayWay(string orderNumbers)
        {
            PcpSession.ClearOrder();
            var order = _orderService.CreateTransientOrder(orderNumbers, PcpSession.CurrentUser);
            PcpSession.Order = order;

            return View();
        }

        public ActionResult UserInfoConfirmation()
        {
            var model = new UserInfoConfirmationViewModel
                {
                    Id=PcpSession.CurrentUser.Id,
                    Email = PcpSession.CurrentUser.Email,
                    TaxNumber = PcpSession.CurrentUser.TaxNumber
                };
            return PartialView(model);
        }

        public ActionResult TerminalPayInfo()
        {
            var commision = _osmpPaymentService.GetCommission();
            var totalPrice = _orderService.CalculateCommission(PcpSession.Order, commision);

            var model = new TerminalPayInfoViewModel
                {
                    Commission = commision,
                    TotalPrice = totalPrice,
                    Violations = _viewModelCreator.Create<IList<ViolationViewModel>, IList<OrderDetail>>(PcpSession.Order.Details.ToList())
                };

            foreach (var violation in model.Violations)
            {
                var violationInfo = _violationService.GetViolationsByOrder(violation.OrderNumber, PcpSession.CurrentUser.CarNumber);
                violation.CarMark = violationInfo.Mark;
                violation.Cost = (decimal)violationInfo.FineCost;
                violation.PostAddress = violationInfo.PostAddress;
                violation.ViolationType = violationInfo.ViolationType;
                violation.FixationDateTime = violation.FixationDateTime;

            }

           

            return View(model);
        }

        [HttpPost]
        public ActionResult UserInfoConfirmation(UserInfoConfirmationViewModel userInfoConfirmation)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        view = this.RenderPartialView("UserInfoConfirmation", userInfoConfirmation)
                    }
                };
            }

            PcpSession.CurrentUser.TaxNumber = userInfoConfirmation.TaxNumber;
            PcpSession.CurrentUser.Email = userInfoConfirmation.Email;
            _userService.UpdateUser(PcpSession.CurrentUser);

            return new JsonResult
            {
                Data = new
                {
                    success = true,
                }
            };
        }

        [OutputCache(NoStore = true, Duration = 0)]
        [Obsolete]
        public ActionResult FillEpayForm()
        {
            _orderService.CreateOrder(PcpSession.Order);

            var price = _orderService.CalculateCost(PcpSession.Order);
            var commission = _epayService.GetCommission();
            var priceWithCommission = _orderService.CalculateCommission(price, commission);

            return new JsonResult
            {
                Data = new
                {
                    success = true,
                   
                    signedString = EPayKkb.Build64(PcpSession.Order.EpayOrderId, priceWithCommission),
                    appendix = BuildAppendixField(PcpSession.Order, commission),
                    email = PcpSession.CurrentUser.Email
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private string BuildAppendixField(Order order, double commission)
        {
            var xml = "<document>";
            var count = 1;
            foreach (var orderDetail in order.Details)
            {
                var priceWithCommission = orderDetail.Cost + orderDetail.Cost*(decimal) commission/100m;
                xml += "<item number=\"" + count + "\" name=\"" + SecurityElement.Escape(orderDetail.OrderNumber) + "\" quantity=\"" + 1 + "\" amount=\"" + priceWithCommission + "\"/>";
                count++;
            }

            xml += "</document>";

            var bytes = new UTF8Encoding().GetBytes(xml);
            return Convert.ToBase64String(bytes, Base64FormattingOptions.None);
        }
    }
}