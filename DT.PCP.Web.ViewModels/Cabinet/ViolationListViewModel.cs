using System.Collections.Generic;
using System.Linq;

namespace DT.PCP.Web.ViewModels.Cabinet
{
    public class ViolationListViewModel
    {
        public bool ShowPayed { get; set; }

        public string CarColor { get; set; }

        public string CarMark { get; set; }

        public string CarNumber { get; set; }

        public IEnumerable<CarViolationViewModel> Violations { get; set; }
    }
}
