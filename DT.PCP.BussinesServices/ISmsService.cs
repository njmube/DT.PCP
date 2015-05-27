using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.PCP.BussinesServices
{
    public interface ISmsService
    {
        void SendSms(string phoneNumber, string htmlBody);

    }
}
