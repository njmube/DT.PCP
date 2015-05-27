using System.Web.Mvc;
using DT.PCP.Web.ViewModels.Account;

namespace DT.PCP.Web.Portal.Controllers
{
    public class ViolationController : BaseController
    {
        public ActionResult Index(LoginViewModel model)
        {
            return View();
        }

    }
}
