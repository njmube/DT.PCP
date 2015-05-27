using System;
using System.Runtime.Serialization;

namespace DT.PCP.Services.Objects
{
    [DataContract]
    public class PaymentInfo
    {
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public DateTime PaymentDate { get; set; }
        [DataMember]
        public string RefID { get; set; }
    }
}
