using System.Web.Mvc;
using System.Web.Security;
using DT.PCP.BussinesServices;
using DT.PCP.CommonDomain;
using DT.PCP.Web.Core;
using DT.PCP.Web.ViewModels.Account;

namespace DT.PCP.Web.Portal.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult ChangeCulture(Culture lang, string returnUrl)
        {
            if (returnUrl.Length >= 3)
            {
                returnUrl = returnUrl.Substring(3);
            }
            return Redirect("/" + lang.ToString() + returnUrl);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginByOrder(LoginViewModel model)
        {
            var status = _userService.CreateUserByOrder(model.CarNumber, model.OrderNumber);
            string message = string.Empty;
            var isSuccess = false;
            switch (status)
            {
                case StatusUser.ObjectSpaceIsNull:
                    message = this.RenderPartialView("ServerNotRespond", null);
                    break;
                case StatusUser.TsNotFound:
                    message = this.RenderPartialView("TsNotFound", null);
                    break;
                default:
                    PcpSession.CurrentUser = _userService.GetUser(model.CarNumber);
                    FormsAuthentication.SetAuthCookie(string.Format("{0} - {1}", PcpSession.CurrentUser.CarNumber, PcpSession.CurrentUser.CarPassportNumber), true);
                    isSuccess = true;
                    break;
            }

            return new JsonResult
            {
                Data = new
                {
                    success = isSuccess,
                    message = message
                }
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            StatusUser status = StatusUser.TsFound;
            string message = string.Empty;
            var isSuccess = false;
            if (!_userService.ValidateUser(model.CarNumber, model.PassportNumber))
                status =_userService.CreateUser(model.CarNumber, model.PassportNumber);

            switch (status)
            {
                case StatusUser.ObjectSpaceIsNull:
                    message = this.RenderPartialView("ServerNotRespond", null);
                    break;
                case StatusUser.TsNotFound:
                    message = this.RenderPartialView("TsNotFound", null);
                    break;
                case StatusUser.SrtsNotEqual:
                    message = this.RenderPartialView("SrtsNotEqual", null);
                    break;
                case StatusUser.SrtsNotFound:
                    message = this.RenderPartialView("SrtsNotFound", null);
                    break;
                default:
                    FormsAuthentication.SetAuthCookie(string.Format("{0} - {1}", model.CarNumber, model.PassportNumber), true);
                    PcpSession.CurrentUser = _userService.GetUser(model.CarNumber, model.PassportNumber);
                    isSuccess = true;
                    break;
            }
           
            return new JsonResult
            {
                Data = new
                {
                    success = isSuccess,
                    message = message                
                }
            };

           
        }

       
        [HttpPost]
        
        public ActionResult LogOff()
        {
           FormsAuthentication.SignOut();
           PcpSession.ClearCurrentUser();
            PcpSession.Abandon();

            return RedirectToAction("Index", "Home");
        }

        
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                if (string.IsNullOrWhiteSpace(returnUrl))
                    return RedirectToAction("Index", "Cabinet");
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
