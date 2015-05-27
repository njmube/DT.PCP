using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DT.PCP.DataAccess;
using DT.PCP.DataAccess.Impl;
using DT.PCP.Domain;
using DT.PCP.Utils.Impl;
using DT.PCP.Web.Core.Filters;

namespace DT.PCP.Web.Portal.Controllers
{
    [DetectNewSessionFilterAttrubute]
    public class BaseController:Controller
    {
        public IRepository Repository { get; set; }

        public BaseController()
        {
            Repository = new EfRepository(new PcpContext());
            var count = Repository.Query<OrderDetail>().Count(d => d.Order.IsPayed) +
                                         int.Parse(ConfigurationManager.AppSettings["StartCount"]);
            ViewData["PayedViolation"] = count;

            ViewData["DeclineNumber"] = count.Decline("нарушение", "нарушения", "нарушений");

        }
        
    }
}