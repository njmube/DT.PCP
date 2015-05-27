using System.ServiceModel;
using DT.PCP.Services.Objects;

namespace DT.PCP.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOsmpService" in both code and config file together.
    [ServiceContract]
    public interface IOsmpService
    {
        [OperationContract]
        AccountInfo GetAccountInfo(string opCode, string account);

        [OperationContract]
        ProcessPaymentResult ProcessPayment(string opCode, PaymentInfo payInfo);
    }
}
