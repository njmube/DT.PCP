using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.PCP.CommonDomain
{
    public class EmailNotificationSettings:BaseNotificationSettings
    {
        public bool EmailNotification { get; set; }
        public string Email { get; set; }
    }
}
