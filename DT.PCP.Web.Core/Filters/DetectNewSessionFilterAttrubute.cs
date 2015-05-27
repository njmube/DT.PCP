using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DT.PCP.BussinesServices;

namespace DT.PCP.Web.Core.Filters
{
    public class DetectNewSessionFilterAttrubute:ActionFilterAttribute
    {
        public IUserService UserService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if(filterContext.HttpContext.Request.IsAuthenticated && PcpSession.CurrentUser == null)
            {
                var combinedNumbers = filterContext.HttpContext.User.Identity.Name;
                var carNumber = combinedNumbers.Split('-')[0].Trim();
                var carPassportNumber = combinedNumbers.Split('-')[1].Trim();
                PcpSession.CurrentUser = UserService.GetUser(carNumber, carPassportNumber);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
