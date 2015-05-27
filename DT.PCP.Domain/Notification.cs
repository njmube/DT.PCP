using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.PCP.Domain
{
    public class Notification:Entity
    {
        public DateTime? LastNotification { get; set; }
        public string UnsubscribeHash { get; set; }
        public int NotificationCode { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual User User { get; set; }
        
    }
}
