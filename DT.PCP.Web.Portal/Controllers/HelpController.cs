using System.Web.Mvc;

namespace DT.PCP.Web.Portal.Controllers
{
    public class HelpController : BaseController
    {
        public ActionResult LoginBothInfo()
        {
            return View("Instructions/_LoginBoth");
        }

        public ActionResult LoginInfoCarNumber()
        {
            return View("Instructions/_LoginCarNumberPassport");
        }

        public ActionResult LoginInfoOrderNumber()
        {
            return View("Instructions/_LoginCarNumberOrder");
        }

        public ActionResult ChangeUserInfo()
        {
            return View("Instructions/_ChaneUserInfo");
        }

        public ActionResult SubscribeInfo()
        {
            return View("Instructions/_Subscribe");
        }

        public ActionResult UnSubscribeInfo()
        {
            return View("Instructions/_UnSubscribe");
        }

        public ActionResult ChangeSubscribeInfo()
        {
            return View("Instructions/_ChangeSubscribe");
        }

        public ActionResult PayCardInfo()
        {
            return View("Instructions/_PayCard");
        }

        public ActionResult PayTerminalInfo()
        {
            return View("Instructions/_PayTerminal");
        }

        public ActionResult PayEmoneyInfo()
        {
            return View("Instructions/_PayEMoney");
        }

        public ActionResult Faq()
        {
            return View("Instructions/_Faq");
        }

        public ActionResult PaySuccessInfo()
        {
            return View("Instructions/_PaySuccess");
        }

        public ActionResult ProfileInfo()
        {
            return View("Instructions/_Profile");
        }
        public ActionResult ViolationListInfo()
        {
            return View("Instructions/_ViolationList");
        }
        
        public ActionResult LoginInfo()
        {
            return PartialView();
        }

        public ActionResult EditInfo()
        {
            return PartialView();
        }

        public ActionResult CheckInfo()
        {
            return PartialView();
        }

        public ActionResult NotificationInfo()
        {
            return PartialView();
        }



    }
}
