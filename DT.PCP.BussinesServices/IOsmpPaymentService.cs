using DT.PCP.CommonDomain;

namespace DT.PCP.BussinesServices
{
    public interface IOsmpPaymentService
    {
        void AddOperationInfo(string account, OsmpOperationStatus status, string opCode, OsmpMethod method);
        double GetCommission();
    }
}
