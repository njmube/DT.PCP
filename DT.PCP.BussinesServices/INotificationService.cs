using System.Collections.Generic;
using DT.PCP.CommonDomain;
using DT.PCP.Domain;

namespace DT.PCP.BussinesServices
{
    public interface INotificationService
    {
        UpdateNotificationResult UpdateNotification(User currentUser, NotificationSetting notificationSetting);
        User ConfirmNotification(User user, int code);
        UpdateNotificationResult UpdateNotification(User currentUser,  User changedUser);
        IList<Notification> GetSubsribed();
        void UpdateNotification(Notification notification);

    }
}
