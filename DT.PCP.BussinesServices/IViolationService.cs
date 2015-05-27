using System.Drawing;
using DT.PCP.CommonDomain;
using DT.PCP.Domain;
using DT.PCP.ServicesProxies.BddViolationService;


namespace DT.PCP.BussinesServices
{
    public interface IViolationService
    {
        TransportOwnerData GetOwnerInfo(string carNumber, string carPassportNumber);
        TrafficViolationServiceResult GetViolations(string carNumber, string carPassportNumber);
        TrafficViolationData GetViolationsByOrder(string orderNumber, string carNumber);
        bool RegisterPayInBdd(PayedViolation payedViolation);
        bool RegisterPayInBdd(Order payedViolation);
        string GetViolationImagePath(string orderNumber);
    }
}
