using System;

namespace DT.PCP.CommonDomain
{
    public class EpayResponse
    {
        /// <summary>
        /// Код ответа
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// Служебное поле
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Служебное поле
        /// </summary>
        public string ApprovalCode { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Цена зкаказа
        /// </summary>
        public decimal CostFull { get; set; }

        /// <summary>
        /// Идентификатор, который был передан в epay
        /// </summary>
        public string EpayOrderId { get; set; }
    }
}
