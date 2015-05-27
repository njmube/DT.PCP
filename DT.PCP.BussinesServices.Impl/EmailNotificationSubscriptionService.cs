using System;
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
    public class EmailNotificationSubscriptionService : INotificationSubscriptionService<EmailNotificationSettings, EmailNotification>
    {
        private readonly IRepository _repository;
        private readonly IEmailService _emailService;

        public EmailNotificationSubscriptionService(IRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public UpdateNotificationResult SubscribeOrUnsubscribe(User user, User changedUser, int checkCode)
        {
            var emailNotification = user.Notifications.OfType<EmailNotification>().FirstOrDefault();
            var settings = new EmailNotificationSettings
                {
                    Code = checkCode,
                    Email = changedUser.Email,
                    EmailNotification = emailNotification != null && emailNotification.IsConfirmed
                };

            return SubscribeOrUnsubscribe(user, settings,
                                          user.Notifications.OfType<EmailNotification>().FirstOrDefault());
        }

        public UpdateNotificationResult SubscribeOrUnsubscribe(User user, EmailNotificationSettings settings, EmailNotification notification)
        {
            user = _repository.Query<User>().FirstOrDefault(
                               u => u.CarNumber == user.CarNumber && u.CarPassportNumber == user.CarPassportNumber);
            if (user == null)
                throw new Exception("Пользователь не найден");

            if (notification == null)
            {
                notification = new EmailNotification { User = user };
                user.Notifications.Add(notification);
            }
            
            if (SettingsNotChanged(settings, notification))
                return new UpdateNotificationResult
                    {
                        ShowConfirmation = false,
                        User = user
                    };

            if (EmailChanged(settings, notification))
            {
                user.Email = settings.Email;
            }

            if (NeedToSendCode(settings, notification))
            {
                // отправляем код
              
                user = UpdateNotification(user, settings.Code);

                var templateView = Template.NotificationCheckCode;
                var view = RazorParser.ParseView(templateView, settings.Code);
                _emailService.SendEmail(settings.Email, "Подтверждение подписки на уведомления", view);

                return new UpdateNotificationResult
                {
                    ShowConfirmation = true,
                    User = user
                };

            }
            if (UserUnsubscribed(settings, notification))
            {
                // отправляем сообщение о снятии подписки
                user = UpdateNotification(user, 0);

                var templateView = Template.NotificationUnsubscribe;
                var view = RazorParser.ParseView(templateView, user);
                _emailService.SendEmail(settings.Email, "Отписка на уведомления о нарушениях", view);

                return new UpdateNotificationResult
                {
                    ShowConfirmation = false,
                    User = user
                };
            }

            return new UpdateNotificationResult
            {
                ShowConfirmation = false,
                User = user
            };

        }

        private User UpdateNotification(User user, int checkCode)
        {
            var emailNotification = user.Notifications.OfType<EmailNotification>().FirstOrDefault();
            if (emailNotification == null)
                throw new Exception("Объект EmailNotification не найден");

            emailNotification.IsConfirmed = false;
            emailNotification.NotificationCode = checkCode;
            _repository.Update(user);
            _repository.SaveChanges();
            return user;
        }
        
        private bool NeedToSendCode(EmailNotificationSettings settings, EmailNotification notification)
        {
            var isSubscribed = settings.EmailNotification && notification.IsConfirmed && notification.User.Email != settings.Email;
            if (isSubscribed) return true;
            return !notification.IsConfirmed && settings.EmailNotification;
        }

        private bool UserUnsubscribed(EmailNotificationSettings settings, EmailNotification notification)
        {
            return notification.IsConfirmed && !settings.EmailNotification &&
                   notification.User.Email == settings.Email;
        }



        private bool EmailChanged(EmailNotificationSettings settings, EmailNotification notification)
        {
            return settings.Email != notification.User.Email;
        }

        private bool SettingsNotChanged(EmailNotificationSettings settings, EmailNotification notification)
        {
            return settings.Email == notification.User.Email && settings.EmailNotification == notification.IsConfirmed;
        }
    }
}
