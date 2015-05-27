using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Wcf;
using DT.PCP.Services;
using DT.PCP.Utils.Impl;

namespace DT.PCP.ServiceHost
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        private ContainerBuilder _builder;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RegisterTypes();
            AutomapBootstrap.InitializeMap();
        }

        private void RegisterTypes()
        {
            _builder = new ContainerBuilder();
            _builder.RegisterType<OsmpService>().As<IOsmpService>();

            AutofacHostFactory.Container = _builder.Build();

        }
    }
}