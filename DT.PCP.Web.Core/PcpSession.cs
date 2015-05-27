using System.Web;
using DT.PCP.Domain;

namespace DT.PCP.Web.Core
{
    public static class PcpSession
    {
        private const string _currentUser = "CurrentUser";
        private const string _order = "Order";

        public static User CurrentUser
        {
            get
            {
                var user = HttpContext.Current.Session[_currentUser];
                if (user != null)
                    return (User)user;
                return null;
            }
            set
            {
                HttpContext.Current.Session[_currentUser] = value;
            }
        }

       

        public static Order Order
        {
            get
            {
                var selectedViolations = HttpContext.Current.Session[_order];
                if (selectedViolations != null)
                    return (Order)selectedViolations;
                return null;
            }
            set
            {
                HttpContext.Current.Session[_order] = value;
            }
        }


        public static void ClearCurrentUser()
        {
            CurrentUser = null;
        }

        public static void ClearOrder()
        {
            Order = null;
        }

        public static void Abandon()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
