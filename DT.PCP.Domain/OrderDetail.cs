namespace DT.PCP.Domain
{
    /// <summary>
    /// Детали заказа
    /// </summary>
    public class OrderDetail:Entity
    {
        /// <summary>
        /// Номер предписания
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Цена штрафа
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        public Order Order { get; set; }
    }
}
