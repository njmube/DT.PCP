using System;

namespace DT.PCP.Web.ViewModels
{
    public class ViolationNotificationModel
    {
        public DateTime FixationDateTime { get; set; }
        public string ViolatorFullName { get; set; }
        public string PostAddress { get; set; }
        public string ViolationType { get; set; }
        public string ViolationTypeShort { get; set; }
        public string OrderNumber { get; set; }
        public string Cost { get; set; }
        public string UnsubscribeHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool SmsNotification { get; set; }
        public bool EmailNotification { get; set; }
        public bool IsLegalEntity { get; set; }
    }
}
