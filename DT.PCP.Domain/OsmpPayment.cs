using System;

namespace DT.PCP.Domain
{
    public class OsmpPayment:Entity
    {
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
