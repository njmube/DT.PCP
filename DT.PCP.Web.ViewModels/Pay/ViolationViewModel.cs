using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Pay;

namespace DT.PCP.Web.ViewModels.Pay
{
    public class ViolationViewModel
    {
        /// <summary>
        /// Номер предписания
        /// </summary>
        [LocalizedDisplayName("OrderNumber", NameResourceType = typeof(PayViewModelsStrings))]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Марка машины
        /// </summary>
        public string CarMark { get; set; }

        /// <summary>
        /// Дата фиксации нарушения
        /// </summary>
        public string FixationDateTime { get; set; }

        /// <summary>
        /// Тип нарушения
        /// </summary>
        public string ViolationType { get; set; }

        /// <summary>
        /// Адрес нарушения
        /// </summary>
        public string PostAddress { get; set; }

        /// <summary>
        /// Сумма штрафа
        /// </summary>
        [LocalizedDisplayName("Cost", NameResourceType = typeof(PayViewModelsStrings))]
        public decimal Cost { get; set; }

    }
}
