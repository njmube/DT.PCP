using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace DT.PCP.ViolationNotificationWorker
{
    public class JobFactory:IJobFactory
    {
        private readonly IContainer _container;

        public JobFactory(IContainer container)
        {
            _container = container;
        }

        public CheckNewViolationJob CreateCheckNewViolationJob()
        {
            return _container.Resolve<CheckNewViolationJob>();
        }
    }
}
