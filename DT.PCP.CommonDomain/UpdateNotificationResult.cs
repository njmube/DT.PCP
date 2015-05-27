using DT.PCP.Domain;

namespace DT.PCP.CommonDomain
{
    public class UpdateNotificationResult
    {
        public User User { get; set; }
        public bool ShowConfirmation { get; set; }
    }
}
