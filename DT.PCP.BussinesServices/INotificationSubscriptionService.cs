using DT.PCP.CommonDomain;
using DT.PCP.Domain;

namespace DT.PCP.BussinesServices
{
    public interface INotificationSubscriptionService<TSettings, TNotification>
        where TSettings : BaseNotificationSettings
        where TNotification : Notification
    {
        UpdateNotificationResult SubscribeOrUnsubscribe(User user, TSettings settings, TNotification notification);
        UpdateNotificationResult SubscribeOrUnsubscribe(User user, User changedUser, int checkCode);
    }
}
