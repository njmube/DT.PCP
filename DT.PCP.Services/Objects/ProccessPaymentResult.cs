using System.Runtime.Serialization;
using DT.PCP.CommonDomain;

namespace DT.PCP.Services.Objects
{
    [DataContract]
    public class ProcessPaymentResult
    {
        public ProcessPaymentResult(PaymentInfo payment, OsmpOperationStatus status, int paymentID)
        {
            Payment = payment;
            Status = status;
            PaymentID = paymentID;
        }

        [DataMember]
        public PaymentInfo Payment { get; private set; }
        [DataMember]
        public OsmpOperationStatus Status { get; private set; }
        [DataMember]
        public int PaymentID { get; private set; }
    }
}
