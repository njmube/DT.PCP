using System.ComponentModel.DataAnnotations;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Cabinet;

namespace DT.PCP.Web.ViewModels.Cabinet
{
    public class ConfirmationNotificationViewModel
    {
        [Required(ErrorMessageResourceName = "ValidationNotificationCode", ErrorMessageResourceType = typeof(CabinetViewModelsStrings))]
        [LocalizedDisplayName("ConfirmationNotificationCode", NameResourceType = typeof(CabinetViewModelsStrings))]
        public int Code { get; set; }
    }
}
