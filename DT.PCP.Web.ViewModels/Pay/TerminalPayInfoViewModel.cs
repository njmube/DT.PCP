using System.Collections.Generic;

namespace DT.PCP.Web.ViewModels.Pay
{
    public class TerminalPayInfoViewModel
    {
        /// <summary>
        /// Информация о нарушениях
        /// </summary>
        public IList<ViolationViewModel> Violations { get; set; }

        public double Commission { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
