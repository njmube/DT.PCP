using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Cabinet;

namespace DT.PCP.Web.ViewModels.Cabinet
{
    public class NotificationViewModel : IValidatableObject
    {
        [LocalizedDisplayName("Email", NameResourceType = typeof(CabinetViewModelsStrings))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(CabinetViewModelsStrings),
            ErrorMessageResourceName = "ValidationEmail")]
        public string Email { get; set; }

        [LocalizedDisplayName("PhoneNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        [RegularExpression(@"(\+)?77[0-9]{9}", ErrorMessageResourceType = typeof(CabinetViewModelsStrings),
            ErrorMessageResourceName = "ValidationPhoneNumber")]
        public string Phone { get; set; }

        public bool SmsNotification { get; set; }
        public bool EmailNotification { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           
            if(SmsNotification && string.IsNullOrEmpty(Phone))
                yield return new ValidationResult("", new[] {"Phone"});
            if (EmailNotification && string.IsNullOrEmpty(Email))
                yield return new ValidationResult("", new[] {"Email"});
           
        }
    }
}
