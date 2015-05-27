using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DT.PCP.CommonDomain;
using DT.PCP.DataAccess;
using DT.PCP.Domain;
using DT.PCP.Utils.Impl;

namespace DT.PCP.BussinesServices.Impl
{
    public class SmsNotificationSubscriptionService : INotificationSubscriptionService<SmsNotificationSettings, SmsNotification>
    {
        private readonly IRepository _repository;
        private readonly ISmsService _smsService;

        public SmsNotificationSubscriptionService(IRepository repository, ISmsService smsService)
        {
            _repository = repository;
            _smsService = smsService;
        }

        public UpdateNotificationResult SubscribeOrUnsubscribe(User user, User changedUser, int checkCode)
        {
            var smsNotification = user.Notifications.OfType<SmsNotification>().FirstOrDefault();
            var settings = new SmsNotificationSettings()
            {
                Code = checkCode,
                Phone = changedUser.PhoneNumber,
                SmsNotification = smsNotification != null && smsNotification.IsConfirmed
            };

            return SubscribeOrUnsubscribe(user, settings,
                                          user.Notifications.OfType<SmsNotification>().FirstOrDefault());
        }

        public UpdateNotificationResult SubscribeOrUnsubscribe(User user, SmsNotificationSettings settings,
                                                               SmsNotification notification)
        {
            user = _repository.Query<User>().FirstOrDefault(
                              u => u.CarNumber == user.CarNumber && u.CarPassportNumber == user.CarPassportNumber);
            if (user == null)
                throw new Exception("Пользователь не найден");

            if (notification == null)
            {
                notification = new SmsNotification { User = user };
                user.Notifications.Add(notification);
            }

            if (SettingsNotChanged(settings, notification))
                return new UpdateNotificationResult
                {
                    ShowConfirmation = false,
                    User = user
                };

            if (PhoneChanged(settings, notification))
            {
                user.PhoneNumber = settings.Phone;
            }

            if (NeedToSendCode(settings, notification))
            {
                // отправляем код
                user = UpdateNotification(user, settings.Code);
                _smsService.SendSms(settings.Phone, "Код подтверждения: " + settings.Code);
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
                _smsService.SendSms(settings.Phone, "Вы успешно отписались от SMS уведомлений");
            }

            return new UpdateNotificationResult
            {
                ShowConfirmation = false,
                User = user
            };
        }

        private User UpdateNotification(User user, int checkCode)
        {
            var smsNotification = user.Notifications.OfType<SmsNotification>().FirstOrDefault();
            if (smsNotification == null)
                throw new Exception("Объект SmsNotification не найден");

            smsNotification.IsConfirmed = false;
            smsNotification.NotificationCode = checkCode;
            _repository.Update(user);
            _repository.SaveChanges();
            return user;
        }

       

        private bool NeedToSendCode(SmsNotificationSettings settings, SmsNotification notification)
        {
            var isSubscribed = settings.SmsNotification && notification.IsConfirmed && notification.User.PhoneNumber != settings.Phone;
            if (isSubscribed) return true;
            return !notification.IsConfirmed && settings.SmsNotification;
        }

        private bool UserUnsubscribed(SmsNotificationSettings settings, SmsNotification notification)
        {
            return notification.IsConfirmed && !settings.SmsNotification &&
                   notification.User.PhoneNumber == settings.Phone;
        }



        private bool PhoneChanged(SmsNotificationSettings settings, SmsNotification notification)
        {
            return settings.Phone != notification.User.PhoneNumber;
        }

        private bool SettingsNotChanged(SmsNotificationSettings settings, SmsNotification notification)
        {
            return settings.Phone == notification.User.PhoneNumber && settings.SmsNotification == notification.IsConfirmed;
        }


    }
}
