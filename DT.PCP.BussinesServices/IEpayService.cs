using System.Collections.Generic;
using DT.PCP.CommonDomain;
using DT.PCP.Domain;

namespace DT.PCP.BussinesServices
{
    public interface IEpayService
    {
        /// <summary>
        /// Проверка и фиксация платежа в epay
        /// </summary>
        /// <param name="xml">Ответ от банка в XML формате</param>
        /// <returns></returns>
        bool FixPayment(string xml);

        EpayResponse ParseResponse(string response);

        double GetCommission();

    }
}
