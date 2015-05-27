using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
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
using DT.PCP.Web.Core.Filters;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace DT.PCP.Web.Portal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private ContainerBuilder _builder;
        private IContainer _container;
        public static string PathToCertificate { get; set; }
        protected void Application_Start()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            CustomTheme.ThemesProviderEx.Register();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            SetupContainer();
            ConfigureLogger();

            AutomapBootstrap.InitializeMap();
        }

        /// <summary>
        /// Настройка контейнера, регистрация типов
        /// </summary>
        private void SetupContainer()
        {
            _builder = new ContainerBuilder();
            _builder.RegisterFilterProvider();

            RegisterControllers();
            RegisterTypes();
            RegisterExternalServices();

            _container = _builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        /// <summary>
        /// Регистрирует типы в контейнере
        /// </summary>
        private void RegisterTypes()
        {
            _builder.RegisterType<Logger>().As<ILogger>();
            _builder.RegisterType<PcpContext>().AsSelf();
                
            _builder.RegisterType<EfRepository>().As<IRepository>().InstancePerHttpRequest();


            _builder.RegisterType<UserService>().As<IUserService>();
            _builder.RegisterType<EmailNotificationSubscriptionService>().As<INotificationSubscriptionService<EmailNotificationSettings, EmailNotification>>();
            _builder.RegisterType<SmsNotificationSubscriptionService>().As<INotificationSubscriptionService<SmsNotificationSettings, SmsNotification>>();
            _builder.RegisterType<ViolationService>().As<IViolationService>();
            _builder.RegisterType<OrderService>().As<IOrderService>();
            _builder.RegisterType<EpayService>().As<IEpayService>();
            _builder.RegisterType<EmailService>().As<IEmailService>();
            _builder.RegisterType<SmsService>().As<ISmsService>();
            _builder.RegisterType<NotificationService>().As<INotificationService>();
            _builder.RegisterType<OsmpPaymentService>().As<IOsmpPaymentService>();
            _builder.RegisterType<ViewModelCreator>().As<IViewModelCreator>();
            _builder.RegisterType<EntityCreator>().As<IEntityCreator>();
            

            _builder.RegisterType<DetectNewSessionFilterAttrubute>().PropertiesAutowired();
        }

        public void RegisterExternalServices()
        {
            _builder.RegisterType<TrafficViolationServiceClient>().As<ITrafficViolationService>();
            _builder.RegisterType<TrafficViolationPayRegisterClient>().As<ITrafficViolationPayRegister>();
        }

        /// <summary>
        ////Регистрацация всех контроллеров в контейнере
        /// </summary>
        private void RegisterControllers()
        {
            _builder.RegisterControllers(Assembly.GetExecutingAssembly()).InstancePerHttpRequest();
        }

        private void ConfigureLogger()
        {
            Logger.Configure(Server.MapPath("~/App_Data/nlog.cfg.xml"));

        }

        

        
    }
}