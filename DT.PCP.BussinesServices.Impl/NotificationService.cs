using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DT.PCP.CommonDomain;
using DT.PCP.DataAccess;
using DT.PCP.Domain;
using DT.PCP.NotificationTemplate;
using DT.PCP.Utils.Impl;

namespace DT.PCP.BussinesServices.Impl
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IUserService _userService;
        private readonly IRepository _repository;

        public NotificationService(IEmailService emailService, ISmsService smsService, IUserService userService, IRepository repository)
        {
            _emailService = emailService;
            _smsService = smsService;
            _userService = userService;
            _repository = repository;
        }
        
        public IList<Notification> GetSubsribed()
        {
            return _repository.NotTrackedQuery<Notification>().Where(n => n.IsConfirmed).ToList();
        }
        [Obsolete]
        public void UpdateNotification(Notification notification)
        {
            _repository.Update(notification);
            _repository.SaveChanges();
        }
         [Obsolete]
        public UpdateNotificationResult UpdateNotification(User currentUser, User changedUser)
        {
            var emailNotification = currentUser.Notifications.OfType<EmailNotification>().FirstOrDefault();
            var smsNotification = currentUser.Notifications.OfType<SmsNotification>().FirstOrDefault();
            var notificationSetting = new NotificationSetting()
                {
                    EmailNotification = emailNotification != null && emailNotification.IsConfirmed,
                    Email = changedUser.Email,
                    SmsNotification = smsNotification != null && smsNotification.IsConfirmed,
                    Phone = changedUser.PhoneNumber
                };

            return UpdateNotification(currentUser, notificationSetting);

        }

         [Obsolete]
        public UpdateNotificationResult UpdateNotification(User currentUser, NotificationSetting notificationSetting)
        {

            UpdateNotificationResult result = null;
            int checkCode = Helpers.GetCode();
            var emailNotification = currentUser.Notifications.OfType<EmailNotification>().FirstOrDefault();
            var smsNotification = currentUser.Notifications.OfType<SmsNotification>().FirstOrDefault();

            if (emailNotification != null && emailNotification.IsConfirmed && notificationSetting.EmailNotification &&
                currentUser.Email != notificationSetting.Email)
                result = SubscribeEmailNotification(currentUser, notificationSetting, checkCode);

            if (smsNotification != null && smsNotification.IsConfirmed && notificationSetting.SmsNotification &&
                currentUser.PhoneNumber != notificationSetting.Phone)
                result = SubscribeSmsNotification(currentUser, notificationSetting,checkCode);

            if ((emailNotification == null || !emailNotification.IsConfirmed) && notificationSetting.EmailNotification)
                result = SubscribeEmailNotification(currentUser, notificationSetting,checkCode);

            if ((smsNotification == null || !smsNotification.IsConfirmed) && notificationSetting.SmsNotification)
                result = SubscribeSmsNotification(currentUser, notificationSetting,checkCode);

            if ((emailNotification != null && emailNotification.IsConfirmed) && !notificationSetting.EmailNotification &&
                currentUser.Email == notificationSetting.Email)
                result = UnSubscribeNotification(currentUser, notificationSetting);

            if ((smsNotification != null && smsNotification.IsConfirmed) && !notificationSetting.SmsNotification &&
               currentUser.PhoneNumber == notificationSetting.Phone)
                result = UnSubscribeSmsNotification(currentUser, notificationSetting);

            if (result == null)
                result = new UpdateNotificationResult
                    {
                        User = currentUser,
                        ShowConfirmation = false
                    };

            return result;

        }

        [Obsolete]
        private UpdateNotificationResult UnSubscribeSmsNotification(User currentUser, NotificationSetting notificationSetting)
        {
            _smsService.SendSms(notificationSetting.Phone, "Вы успешно отписались от SMS уведомлений");
            User updatedUser = _userService.AddOrUpdateSmsNotification(0, notificationSetting, currentUser);
            return new UpdateNotificationResult
            {
                ShowConfirmation = false,
                User = updatedUser
            };
        }
         [Obsolete]
        private UpdateNotificationResult UnSubscribeNotification(User currentUser, NotificationSetting notificationSetting)
        {
            var templateView = Template.NotificationUnsubscribe;
            var view = RazorParser.ParseView(templateView, currentUser);
            _emailService.SendEmail(notificationSetting.Email, "Отписка на уведомления о нарушениях", view);

            User updatedUser = _userService.AddOrUpdateEmailNotification(0, notificationSetting, currentUser);
            return new UpdateNotificationResult
            {
                ShowConfirmation = false,
                User = updatedUser
            };
        }
         [Obsolete]
        private UpdateNotificationResult SubscribeSmsNotification(User currentUser, NotificationSetting notificationSetting, int checkCode)
        {
            _smsService.SendSms(notificationSetting.Phone, "Код подтверждения: " + checkCode);


            User updatedUser = _userService.AddOrUpdateSmsNotification(checkCode, notificationSetting, currentUser);

            return new UpdateNotificationResult
            {
                ShowConfirmation = true,
                User = updatedUser
            };
        }
         [Obsolete]
        private UpdateNotificationResult SubscribeEmailNotification(User currentUser, NotificationSetting notificationSetting, int checkCode)
        {
            var templateView = Template.NotificationCheckCode;
            var view = RazorParser.ParseView(templateView, checkCode);

            _emailService.SendEmail(notificationSetting.Email, "Подтверждение подписки на уведомления", view);




            User updatedUser = _userService.AddOrUpdateEmailNotification(checkCode, notificationSetting, currentUser);

            return new UpdateNotificationResult
            {
                ShowConfirmation = true,
                User = updatedUser
            };
        }

        
        public User ConfirmNotification(User user, int code)
        {
            user = _repository.Query<User>().FirstOrDefault(u => u.CarNumber == user.CarNumber && u.CarPassportNumber == user.CarPassportNumber);
            var notifications = user.Notifications.Where(n => n.NotificationCode == code);
            foreach (var notification in notifications)
            {
                notification.IsConfirmed = true;
                notification.LastNotification = DateTime.Now.AddHours(6);

                if (notification is EmailNotification)
                {
                    notification.UnsubscribeHash = Guid.NewGuid().ToString();
                    var templateView = Template.NotificationSubscribed;
                    var view = RazorParser.ParseView(templateView, user);

                    _emailService.SendEmail(user.Email, "Подписка на уведомления о нарушениях", view);
                }
                if (notification is SmsNotification)
                {
                    _smsService.SendSms(user.PhoneNumber, "Вы успешно подписались на sms уведомления о нарушениях.");
                }
            }
            //_repository.Update(user);
            _repository.SaveChanges();

            return user;
        }


    }
}
