using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Cabinet;

namespace DT.PCP.Web.ViewModels.Cabinet
{
    public class CarViolationDetailsViewModel
    {
        [LocalizedDisplayName("ColumnFullName", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string FullName { get; set; }

        [LocalizedDisplayName("CarMark", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string Mark { get; set; }

        [LocalizedDisplayName("CarColor", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string Color { get; set; }

        [LocalizedDisplayName("CarNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string Number { get; set; }

        [LocalizedDisplayName("CarPassportNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string PassportNumber { get; set; }

        [LocalizedDisplayName("ColumnStatus", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string Status { get; set; }

        public ViolationStatusEnum StatusEnum { get; set; }

        [LocalizedDisplayName("ColumnViolationType", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string ViolationType { get; set; }

        [LocalizedDisplayName("ColumnCost", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string Cost { get; set; }

        [LocalizedDisplayName("ColumnFixationTime", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string FixationDateTime { get; set; }

        [LocalizedDisplayName("ColumnPostAddress", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string PostAddress { get; set; }

        [LocalizedDisplayName("ColumnOrderNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string OrderNumber { get; set; }

        public string StatusImageName { get; set; }
    }
}
