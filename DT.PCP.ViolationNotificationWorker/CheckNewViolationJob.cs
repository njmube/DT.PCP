using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DT.PCP.BussinesServices;
using DT.PCP.Domain;
using DT.PCP.NotificationTemplate;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Utils;
using DT.PCP.Web.ViewModels;
using RazorEngine;

namespace DT.PCP.ViolationNotificationWorker
{
    public class CheckNewViolationJob
    {
        private readonly IUserService _userService;
        private readonly IViolationService _violationService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IViewModelCreator _viewModelCreator;
        private readonly INotificationService _notificationService;

        public CheckNewViolationJob(IUserService userService, IViolationService violationService, IEmailService emailService, ISmsService smsService, IViewModelCreator viewModelCreator, INotificationService notificationService)
        {
            _userService = userService;
            _violationService = violationService;
            _emailService = emailService;
            _smsService = smsService;
            _viewModelCreator = viewModelCreator;
            _notificationService = notificationService;
        }

        public void Run(object stateObject)
        {
            var notifications = _notificationService.GetSubsribed();

            foreach (var notification in notifications)
            {
                var violations = _violationService.GetViolations(notification.User.CarNumber, notification.User.CarPassportNumber);

                if (violations == null) continue;

                foreach (var violationData in violations.TrafficViolationDataList)
                {
                        if (violationData.FixationDateTime <= notification.LastNotification)
                            continue;

                        notification.LastNotification = violationData.FixationDateTime;
                        _notificationService.UpdateNotification(notification);

                        var model = CreateViolationNotificationModel(violationData, notification);

                        MakeNotification(model);
                    
                }
            }
        }

        private ViolationNotificationModel CreateViolationNotificationModel(TrafficViolationData violationData, Notification notification)
        {
            return new ViolationNotificationModel
            {
                Cost = violationData.FineCost.ToString(),
                FixationDateTime = violationData.FixationDateTime,
                PostAddress = violationData.PostAddress,
                OrderNumber = violationData.OrderNumber,
                ViolationType = violationData.ViolationType,
                ViolatorFullName = violationData.ViolatorFullName,
                Email = notification.User.Email,
                UnsubscribeHash = notification.UnsubscribeHash,
                PhoneNumber = notification.User.PhoneNumber,
                EmailNotification = notification is EmailNotification,
                SmsNotification = notification is SmsNotification,
                IsLegalEntity = violationData.IsLegalEntity,
                ViolationTypeShort = violationData.ViolationTypeShort

            };
        }

        private void MakeNotification(ViolationNotificationModel violationModel)
        {

            var smsBody = string.Format("{0} {1};{2}; Предписание:{3}; Сумма:{4}", violationModel.FixationDateTime.ToString("dd.MM.yyyy HH:mm:ss"),
                violationModel.ViolationTypeShort, violationModel.PostAddress, violationModel.OrderNumber, violationModel.Cost);
            var razorView = Template.ViolationEmailNotification;

            var htmlBody = Razor.Parse(razorView, violationModel);

            if (violationModel.SmsNotification)
                _smsService.SendSms(violationModel.PhoneNumber, smsBody);

            if (violationModel.EmailNotification)
                _emailService.SendEmail(violationModel.Email, "Новое нарушение", htmlBody);
        }


    }
}
