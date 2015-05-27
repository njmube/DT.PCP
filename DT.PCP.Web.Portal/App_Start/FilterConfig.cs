using System.Web;
using System.Web.Mvc;
using DT.PCP.Web.Core.Filters;

namespace DT.PCP.Web.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new DetectNewSessionFilterAttrubute());
        }
    }
}