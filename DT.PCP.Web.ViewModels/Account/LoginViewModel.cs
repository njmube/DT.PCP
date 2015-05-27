using DT.PCP.Web.Core;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Account;

namespace DT.PCP.Web.ViewModels.Account
{
    /// <summary>
    /// Представляет модель для входа на сайт
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Номерной знак автомобиля (ГРНЗ ТС)
        /// </summary>
        [LocalizedDisplayName("CarNumber", NameResourceType = typeof(AccountModelsStrings))]
        public string CarNumber { get; set; }

        /// <summary>
        /// Номер техпаспорта (СРТС ТС)
        /// </summary>
         [LocalizedDisplayName("PassportNumber", NameResourceType = typeof(AccountModelsStrings))]
        public string PassportNumber { get; set; }

        public string OrderNumber { get; set; }
        
    }
}