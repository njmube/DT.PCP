using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.PCP.CommonDomain
{
    public class SmsNotificationSettings:BaseNotificationSettings
    {
        public bool SmsNotification { get; set; }
        public string Phone { get; set; }
    }
}
