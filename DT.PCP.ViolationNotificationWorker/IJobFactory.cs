using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.PCP.ViolationNotificationWorker
{
    interface IJobFactory
    {
        CheckNewViolationJob CreateCheckNewViolationJob();
    }
}
