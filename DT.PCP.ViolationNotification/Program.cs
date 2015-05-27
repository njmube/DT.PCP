using System;
using System.Configuration;
using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DT.PCP.BussinesServices;
using DT.PCP.BussinesServices.Impl;
using DT.PCP.DataAccess;
using DT.PCP.DataAccess.Impl;
using DT.PCP.Logging;
using DT.PCP.ServicesProxies.BddPayRegisterService;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Utils;
using DT.PCP.Utils.Impl;

namespace DT.PCP.ViolationNotification
{
    public class Program
    {
        private static Timer _stateTimer;
        private static CheckNewViolationJob _job;
        private static TimerCallback _timerDelegate;
        public static DateTime LastDateTime;
        static IContainer _container;
        static void Main(string[] args)
        {
            RegisterTypes();
           

            IUserService userService = _container.Resolve<IUserService>();
            IViolationService violationService = _container.Resolve<IViolationService>();
            IEmailService emailService = _container.Resolve<IEmailService>();
            ISmsService smsService = _container.Resolve<ISmsService>();
            IViewModelCreator viewModelCreator = _container.Resolve<IViewModelCreator>();
            INotificationService notificationService = _container.Resolve<INotificationService>();
            _job = new CheckNewViolationJob(userService, violationService, emailService, smsService, viewModelCreator,
                notificationService);
          
            _timerDelegate = _job.Run;
            _stateTimer = new Timer(_timerDelegate, null, 1000, int.Parse(ConfigurationManager.AppSettings["CheckInterval"]));
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.X)
                {
                    
                    break;
                }
            }
           
        }

       

        private static void RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<PcpContext>().AsSelf();
            builder.RegisterType<EfRepository>().As<IRepository>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ViolationService>().As<IViolationService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<EpayService>().As<IEpayService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<SmsService>().As<ISmsService>();
            builder.RegisterType<OsmpPaymentService>().As<IOsmpPaymentService>();
            builder.RegisterType<ViewModelCreator>().As<IViewModelCreator>();
            builder.RegisterType<EntityCreator>().As<IEntityCreator>();
            builder.RegisterType<TrafficViolationServiceClient>().As<ITrafficViolationService>();
            builder.RegisterType<TrafficViolationPayRegisterClient>().As<ITrafficViolationPayRegister>();

            _container = builder.Build();
        }
    }


}
