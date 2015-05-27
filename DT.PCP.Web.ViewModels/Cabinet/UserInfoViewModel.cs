using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DT.PCP.Web.Core;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Cabinet;

namespace DT.PCP.Web.ViewModels.Cabinet
{
    public class UserInfoViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [LocalizedDisplayName("CarNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string CarNumber { get; set; }

        [LocalizedDisplayName("CarPassportNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string CarPassportNumber { get; set; }

        [LocalizedDisplayName("CarMark", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string CarMark { get; set; }

        [LocalizedDisplayName("CarModel", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string CarModel { get; set; }

        [LocalizedDisplayName("CarColor", NameResourceType = typeof(CabinetViewModelsStrings))]
        [RegularExpression(@"[-\s\p{L}]+",  ErrorMessageResourceName = "ValdiationWrongData", ErrorMessageResourceType = typeof(CabinetViewModelsStrings))]
        public string CarColor { get; set; }

        [LocalizedDisplayName("CarYear", NameResourceType = typeof(CabinetViewModelsStrings))]
        public int CarYear { get; set; }

        [LocalizedDisplayName("CarRegistrationAddress", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string CarRegistrationAddress { get; set; }

        [LocalizedDisplayName("ArtificialPerson", NameResourceType = typeof(CabinetViewModelsStrings))]
        [RegularExpression(@"[-\s\p{L},.'""0-9]+", ErrorMessageResourceName = "ValdiationWrongData", ErrorMessageResourceType = typeof(CabinetViewModelsStrings))]
        public string ArtificialPerson { get; set; }

        [LocalizedDisplayName("Email", NameResourceType = typeof(CabinetViewModelsStrings))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(CabinetViewModelsStrings),
            ErrorMessageResourceName = "ValidationEmail")]
        public string Email { get; set; }

        [LocalizedDisplayName("PhoneNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        [RegularExpression(@"(\+)?77[0-9]{9}", ErrorMessageResourceType = typeof(CabinetViewModelsStrings),
            ErrorMessageResourceName = "ValidationPhoneNumber")]
        public string PhoneNumber { get; set; }

        [LocalizedDisplayName("FullName", NameResourceType = typeof(CabinetViewModelsStrings))]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
            }
        }

        public bool IsEmailSubscribed { get; set; }
        public bool IsSmsSubscribed { get; set; }

        [LocalizedDisplayName("FirstName", NameResourceType = typeof(CabinetViewModelsStrings))]
        [RegularExpression(@"[-\s\p{L}]+", ErrorMessageResourceName = "ValdiationWrongData", ErrorMessageResourceType = typeof(CabinetViewModelsStrings))]
        public string FirstName { get; set; }

        [LocalizedDisplayName("LastName", NameResourceType = typeof(CabinetViewModelsStrings))]
        [RegularExpression(@"[-\s\p{L}]+", ErrorMessageResourceName = "ValdiationWrongData", ErrorMessageResourceType = typeof(CabinetViewModelsStrings))]
        public string LastName { get; set; }

        [LocalizedDisplayName("MiddleName", NameResourceType = typeof(CabinetViewModelsStrings))]
        [RegularExpression(@"[-\s\p{L}]+", ErrorMessageResourceName = "ValdiationWrongData", ErrorMessageResourceType = typeof(CabinetViewModelsStrings))]
        public string MiddleName { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsSmsSubscribed && string.IsNullOrEmpty(PhoneNumber))
                yield return new ValidationResult("", new[] { "PhoneNumber" });
            if (IsEmailSubscribed && string.IsNullOrEmpty(Email))
                yield return new ValidationResult("", new[] { "Email" });
        }
    }
}