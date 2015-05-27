using DT.PCP.Domain;

namespace DT.PCP.Web.ViewModels.Pay
{
    public class ConfirmOrderViewModel
    {
        public Order Order { get; set; }
        public double CommissionPercent { get; set; }
        public decimal TotalPrice { get; set; }

        public string SignedString { get; set; }

        public string Appendix { get; set; }

        public string Email { get; set; }
    }
}
