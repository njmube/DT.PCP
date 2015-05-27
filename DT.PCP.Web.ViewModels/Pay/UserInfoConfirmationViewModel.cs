using System.ComponentModel.DataAnnotations;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Pay;

namespace DT.PCP.Web.ViewModels.Pay
{
    public class UserInfoConfirmationViewModel
    {
        public int Id { get; set; }

        [LocalizedDisplayName("Email", NameResourceType = typeof(PayViewModelsStrings))]
        [Required(ErrorMessageResourceName = "ValidationRequiredEmail", ErrorMessageResourceType = typeof(PayViewModelsStrings))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(PayViewModelsStrings),
            ErrorMessageResourceName = "ValidationEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ValidationRequiredTaxNumber", ErrorMessageResourceType = typeof(PayViewModelsStrings))]
        [LocalizedDisplayName("TaxNumber", NameResourceType = typeof(PayViewModelsStrings))]
        [StringLength(12, MinimumLength = 12, ErrorMessageResourceType = typeof(PayViewModelsStrings),
            ErrorMessageResourceName = "ValidationTaxNumber")]
        public string TaxNumber { get; set; }
    }
}
