using DT.PCP.CommonDomain;
using DT.PCP.Domain;

namespace DT.PCP.BussinesServices
{
    public interface IEmailService
    {
        void SendEmail(Order order);
        void SendEmail(string email, string subject,  string htmlBody);
        void SendEmail(PayedViolation payedViolation);
    }
}
