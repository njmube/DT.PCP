using DT.PCP.ServicesProxies.BddPayRegisterService;

namespace DT.PCP.CommonDomain
{
    public class PayedViolation
    {
        /// <summary>
        /// Номер предписания
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Полное имя нарушителя
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Сумма штрафа
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// РНН/ИНН/БИК
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Способ оплаты
        /// </summary>
        public PayMethodAlias PayMethod { get; set; }

        /// <summary>
        /// Референс
        /// </summary>
        public string Reference { get; set; }

        public string Email { get; set; }
    }
}
