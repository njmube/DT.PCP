using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DT.PCP.Domain
{
    /// <summary>
    /// Представляет сущность Пользователь
    /// </summary>
    public class User : Entity
    {
        [Required]
        public string CarNumber { get; set; }
        [Required]
        public string CarPassportNumber { get; set; }

        public string CarMark { get; set; }

        public string CarModel { get; set; }

        public string CarColor { get; set; }

        public int CarYear { get; set; }

        public string CarRegistrationAddress { get; set; }

        public string ArtificialPerson { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string TaxNumber { get; set; }

        public bool IsArtificialPerson { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        //public bool SmsNotification { get; set; }

        //public bool EmailNotification { get; set; }

        //public DateTime? LastNotification { get; set; }

        //public string UnsubscribeHash { get; set; }

        //public int  NotificationCode { get; set; }

        //public bool ConfirmedNotification { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName); }
        }

        [NotMapped]
        public string DisplayName
        {
            get
            {

                if (string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(ArtificialPerson))
                    return ArtificialPerson;

                if (!string.IsNullOrWhiteSpace(FirstName))
                    return FirstName;

                return string.Format("{0} - {1}", CarNumber, CarPassportNumber);

            }
        }

    }
}
