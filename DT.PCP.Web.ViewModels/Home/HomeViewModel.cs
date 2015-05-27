﻿using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Account;

namespace DT.PCP.Web.ViewModels.Home
{
    public class HomeViewModel
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

        public int PayedViolationCount { get; set; }
        
    }
}
