using System;
using System.Configuration;
using System.Net;
using System.Threading;
using Autofac;
using DT.PCP.BussinesServices;
using DT.PCP.BussinesServices.Impl;
using DT.PCP.CommonDomain;
using DT.PCP.DataAccess;
using DT.PCP.DataAccess.Impl;
using DT.PCP.Domain;
using DT.PCP.Logging;
using DT.PCP.ServicesProxies.BddPayRegisterService;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Utils;
using DT.PCP.Utils.Impl;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace DT.PCP.ViolationNotificationWorker
{
    public class NotificationWorkerRole : RoleEntryPoint
    {
        readonly CancellationTokenSource cancelSource = new CancellationTokenSource();
        private  Timer _stateTimer;
        private CheckNewViolationJob _job;
        private TimerCallback _timerDelegate;
        public  DateTime LastDateTime;
        private IContainer _container;
        private IJobFactory _jobFactory;

        public override void Run()
        {
            _stateTimer = new Timer(OnTimerTick, null, 1000, int.Parse(ConfigurationManager.AppSettings["CheckInterval"]));
            cancelSource.Token.WaitHandle.WaitOne();
        }

        private void OnTimerTick(object state)
        {
            var job = _container.Resolve<CheckNewViolationJob>();
            job.Run(state);
        }

        public override bool OnStart()
        {
            
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            RegisterTypes();
            return base.OnStart();
        }

        public override void OnStop()
        {
            cancelSource.Cancel();
            base.OnStop();
        }

        private  void RegisterTypes()
        {
            var builder = new ContainerBuilder();
           
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<CheckNewViolationJob>();
            builder.RegisterType<EmailNotificationSubscriptionService>().As<INotificationSubscriptionService<EmailNotificationSettings, EmailNotification>>();
            builder.RegisterType<SmsNotificationSubscriptionService>().As<INotificationSubscriptionService<SmsNotificationSettings, SmsNotification>>();
            
            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<JobFactory>().As<IJobFactory>();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<PcpContext>().AsSelf();
            builder.RegisterType<EfRepository>().As<IRepository>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ViolationService>().As<IViolationService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<EpayService>().As<IEpayService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<SmsService>().As<ISmsService>();
            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<OsmpPaymentService>().As<IOsmpPaymentService>();
            builder.RegisterType<ViewModelCreator>().As<IViewModelCreator>();
            builder.RegisterType<EntityCreator>().As<IEntityCreator>();
            builder.RegisterType<TrafficViolationServiceClient>().As<ITrafficViolationService>();
            builder.RegisterType<TrafficViolationPayRegisterClient>().As<ITrafficViolationPayRegister>();

            _container = builder.Build();
        }
    }
}
