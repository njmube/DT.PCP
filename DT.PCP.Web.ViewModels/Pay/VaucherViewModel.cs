using System;

namespace DT.PCP.Web.ViewModels.Pay
{
    public class VaucherViewModel
    {
        public string OrderNumber { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public string CarNumber { get; set; }
        public string CarPassportNumber { get; set; }
        public string Mark { get; set; }
        public string Color { get; set; }
        public DateTime FixationDateTime { get; set; }
        public string PostAddress { get; set; }
        public string ViolationType { get; set; }
        public decimal Cost { get; set; }
        public bool IsArtificialPerson { get; set; }
        public string FullName { get; set; }
    }
}
