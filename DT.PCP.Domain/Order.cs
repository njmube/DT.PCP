using System;
using System.Collections.Generic;

namespace DT.PCP.Domain
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order:Entity
    {
        /// <summary>
        /// Код заказа для банка
        /// </summary>
        public string EpayOrderId { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Служебное поле при оплате epay
        /// </summary>
        public string ApprovalCode { get; set; }

        /// <summary>
        /// Служебное поле при оплате epay
        /// </summary>
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Оплачен ли заказ
        /// </summary>
        public bool IsPayed { get; set; }

        /// <summary>
        /// Подтверждено ли в БДД
        /// </summary>
        public bool IsConfirmedInBdd { get; set; }

        /// <summary>
        /// Детали заказа
        /// </summary>
        public virtual ICollection<OrderDetail> Details { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual User User { get; set; }

    }
}
