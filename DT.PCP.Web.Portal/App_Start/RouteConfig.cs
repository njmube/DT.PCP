using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DT.PCP.CommonDomain;
using DT.PCP.Web.Core;
using DT.PCP.Web.Core.RouteHandlers;

namespace DT.PCP.Web.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "AboutRoute",
                "About",
                new {controller = "Home", action = "Index"}
                ).RouteHandler = new SingleCultureMvcRouteHandler();

            foreach (Route r in routes)
            {
                if (!(r.RouteHandler is SingleCultureMvcRouteHandler))
                {
                    r.RouteHandler = new MultiCultureMvcRouteHandler();
                    r.Url = "{culture}/" + r.Url;

                    //Добавляем культуру по умолчанию 
                    if (r.Defaults == null)
                    {
                        r.Defaults = new RouteValueDictionary();
                    }
                    r.Defaults.Add("culture", Culture.Ru.ToString());

                    //Добавляем ограничения для параметра культуры
                    if (r.Constraints == null)
                    {
                        r.Constraints = new RouteValueDictionary();
                    }
                    r.Constraints.Add("culture", new CultureConstraint(Culture.En.ToString(), Culture.Ru.ToString(), Culture.Kz.ToString()));
                }
            }
        }
    }
}