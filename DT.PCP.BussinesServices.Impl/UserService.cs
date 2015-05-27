using System;
using System.Collections.Generic;
using System.Linq;
using DT.PCP.CommonDomain;
using DT.PCP.DataAccess;
using DT.PCP.Domain;
using DT.PCP.Logging;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Utils;
using DT.PCP.Utils.Impl;

namespace DT.PCP.BussinesServices.Impl
{
    public class UserService : IUserService
    {
        #region Private fields

        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly IViolationService _violationService;
        private readonly IEntityCreator _entityCreator;

        #endregion


        #region Constructor

        public UserService(ILogger logger, IRepository repository, IViolationService violationService, IEntityCreator _entityCreator)
        {
            _logger = logger;
            _repository = repository;
            _violationService = violationService;
            this._entityCreator = _entityCreator;
        }

        #endregion


        #region Implementation of IUserService

        /// <summary>
        /// Валидация пользователя по номеру машины и номеру техпаспорта
        /// </summary>
        /// <param name="carNumber">Номер машины</param>
        /// <param name="carPassportNumber">Номер техпаспорта</param>
        /// <returns>True если пользователь найден, False в противном случае</returns>
        public bool ValidateUser(string carNumber, string carPassportNumber)
        {
            var user = _repository.Query<User>().FirstOrDefault(u => u.CarNumber == carNumber && u.CarPassportNumber == carPassportNumber);
            return user != null;
        }

        public StatusUser CreateUserByOrder(string carNumber, string orderNumber)
        {
            TrafficViolationData violation;
            try
            {
                violation = _violationService.GetViolationsByOrder(orderNumber, carNumber);

            }
            catch (Exception ex)
            {
                return StatusUser.TsNotFound;
            }

            var user = _repository.Query<User>().FirstOrDefault(
                  u => u.CarNumber == violation.TransportNumber && u.CarPassportNumber == violation.NumberSRTS);

            if (user != null)
                return StatusUser.TsFound;

            user = _entityCreator.Create<User, TrafficViolationData>(violation);
            _repository.Save(user);
            _repository.SaveChanges();

            return StatusUser.TsFound;

        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="carNumber">Номер машины</param>
        /// <param name="carPassportNumber">Номер техпаспорта</param>
        public StatusUser CreateUser(string carNumber, string carPassportNumber)
        {
            StatusUser status;
            try
            {
                TransportOwnerData ownerInfo = _violationService.GetOwnerInfo(carNumber, carPassportNumber);
                User user = _entityCreator.Create<User, TransportOwnerData>(ownerInfo);

                _repository.Save(user);
                _repository.SaveChanges();
                status = StatusUser.TsFound;
            }
            catch (Exception ex)
            {
                _logger.Error("CreateUser: ", ex);
                if (!Enum.TryParse(ex.Message, true, out status))
                    status = StatusUser.ObjectSpaceIsNull;
            }

            return status;
        }

        /// <summary>
        /// Возвращает пользователя по номеру машины
        /// </summary>
        /// <param name="carNumber">Номер машины</param>
        /// <param name="passportNumber"> </param>
        /// <returns></returns>
        public User GetUser(string carNumber, string passportNumber)
        {
            return _repository.Query<User>().IncludeMultiple(c=>c.Notifications).FirstOrDefault(u => u.CarNumber == carNumber && u.CarPassportNumber == passportNumber);
        }

        public User GetUser(string carNumber)
        {
            return _repository.Query<User>().IncludeMultiple(c=>c.Notifications).FirstOrDefault(u => u.CarNumber == carNumber);
        }

        public bool CheckNotificationCode(string carNumber, string carPassportNumber, int code)
        {
            var user = _repository.Query<User>().FirstOrDefault(u => u.CarNumber == carNumber && u.CarPassportNumber == carPassportNumber);
            if (user == null)
                return false;

            var notification = user.Notifications.FirstOrDefault(n=>!n.IsConfirmed && n.NotificationCode == code);
            if (notification == null)
                return false;

            return true;
        }
        
        public void UpdateUser(User user)
        {
            _repository.Update(user);
            _repository.SaveChanges();
        }

        public User GetUserFromService(string carNumber, string carPassportNumber, int userId)
        {
            User user = null;
            try
            {
                _logger.Debug(carNumber);
                _logger.Debug(carPassportNumber);
                TransportOwnerData ownerInfo = _violationService.GetOwnerInfo(carNumber, carPassportNumber);
                user = _entityCreator.Create<User, TransportOwnerData>(ownerInfo);
                user.Id = userId;
            }
            catch (Exception ex)
            {
                _logger.Error("GetUserFromService: Не удалось получить данные от сервиса", ex);
                _logger.Debug(ex.Message);
                _logger.Debug(ex.StackTrace);
                _logger.Debug(ex.Source);
            }

            return user;
        }
        //[Obsolete]
        //public User UpdateNotification(NotificationSetting notificationSetting, User currentUser)
        //{
        //    var user = _repository.Query<User>().FirstOrDefault(
        //         u => u.CarNumber == currentUser.CarNumber && u.CarPassportNumber == currentUser.CarPassportNumber);
        //    if (user == null) return currentUser;

        //    user.SmsNotification = notificationSetting.SmsNotification;
        //    user.EmailNotification = notificationSetting.EmailNotification;
        //    user.Email = notificationSetting.Email;
        //    user.PhoneNumber = notificationSetting.Phone;
        //    user.UnsubscribeHash = Guid.NewGuid().ToString();
        //    user.NotificationCode = currentUser.NotificationCode;
        //    user.ConfirmedNotification = currentUser.ConfirmedNotification;
        //    user.LastNotification = DateTime.UtcNow.AddHours(6);
        //    _repository.Update(user);
        //    _repository.SaveChanges();

        //    return user;

        //}


         public User AddOrUpdateSmsNotification(int checkCode, NotificationSetting notificationSetting,User currentUser)
         {
             var user = _repository.Query<User>().FirstOrDefault(
               u => u.CarNumber == currentUser.CarNumber && u.CarPassportNumber == currentUser.CarPassportNumber);
             if (user == null) return null;

             if (notificationSetting.SmsNotification)
                 user.PhoneNumber = notificationSetting.Phone;

             var existSmsNotification = user.Notifications.OfType<SmsNotification>().FirstOrDefault();
             if (existSmsNotification == null)
             {
                 var smsNotification = new SmsNotification()
                 {
                     NotificationCode = checkCode,
                     LastNotification = DateTime.UtcNow.AddHours(6),
                     UnsubscribeHash = Guid.NewGuid().ToString(),
                     IsConfirmed = false
                 };
                 user.Notifications.Add(smsNotification);
             }
             else
             {
                 existSmsNotification.NotificationCode = checkCode;
                 existSmsNotification.LastNotification = DateTime.UtcNow.AddHours(6);
                 existSmsNotification.UnsubscribeHash = Guid.NewGuid().ToString();
                 existSmsNotification.IsConfirmed = false;
             }

             _repository.SaveChanges();
             return user;
         }


        public User AddOrUpdateEmailNotification(int checkCode, NotificationSetting notificationSetting, User currentUser)
        {
            var user = _repository.Query<User>().FirstOrDefault(
                u => u.CarNumber == currentUser.CarNumber && u.CarPassportNumber == currentUser.CarPassportNumber);
            if (user == null) return null;

            if (notificationSetting.EmailNotification)
                user.Email = notificationSetting.Email;
            
           
            var existEmailNotification = user.Notifications.OfType<EmailNotification>().FirstOrDefault();
            if (existEmailNotification == null)
            {
                var emailNotification = new EmailNotification
                    {
                        NotificationCode = checkCode,
                        LastNotification = DateTime.UtcNow.AddHours(6),
                        UnsubscribeHash = Guid.NewGuid().ToString(),
                        IsConfirmed = false
                    };
                user.Notifications.Add(emailNotification);
            }
            else
            {
                existEmailNotification.NotificationCode = checkCode;
                existEmailNotification.LastNotification = DateTime.UtcNow.AddHours(6);
                existEmailNotification.UnsubscribeHash = Guid.NewGuid().ToString();
                existEmailNotification.IsConfirmed = false;
            }

            _repository.SaveChanges();
            return user;

        }
        
       

        public bool UnsubscribeNotification(string email, string hash)
        {
            var notification =
                _repository.Query<Notification>()
                           .FirstOrDefault(n => n.UnsubscribeHash == hash && n.User.Email == email);

            if (notification == null)
                return false;

            notification.IsConfirmed = false;
            _repository.SaveChanges();
            return true;

        }

        #endregion

        #region Helpers

        /// <summary>
        /// Удаляет лишние символы из номера (-, (), пробелы)
        /// </summary>
        /// <param name="phoneNumber">Телефонный номер</param>
        /// <returns></returns>
        private string NormalizePhoneNumber(string phoneNumber)
        {
            char[] badChars = new[] { '-', '(', ')', ' ' };
            return string.Concat(phoneNumber.Split(badChars, StringSplitOptions.RemoveEmptyEntries));
        }

        #endregion

    }
}
