using System.Collections.Generic;
using DT.PCP.CommonDomain;
using DT.PCP.Domain;

namespace DT.PCP.BussinesServices
{
    public interface IUserService
    {
        bool ValidateUser(string carNumber, string carPassportNumber);
        StatusUser CreateUser(string carNumber, string carPassportNumber);
        StatusUser CreateUserByOrder(string carNumber, string carPassportNumber);
        User GetUser(string carNumber, string passportNumber);
        void UpdateUser(User user);
        User GetUserFromService(string carNumber, string carPassportNumber, int userId);
        User AddOrUpdateEmailNotification(int checkCode, NotificationSetting notificationSetting, User currentUser);
        User AddOrUpdateSmsNotification(int checkCode, NotificationSetting notificationSetting, User currentUser);
        bool UnsubscribeNotification(string email, string hash);
        User GetUser(string carNumber);
        bool CheckNotificationCode(string carNumber, string email, int code);
    }
}
