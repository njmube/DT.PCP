using System;
using System.ComponentModel;
using System.Web.Mvc;
using DT.PCP.Web.Core.Attributes;
using DT.PCP.Web.Resources.Models.Cabinet;
using Lib.Web.Mvc.JQuery.JqGrid;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;
using Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations;

namespace DT.PCP.Web.ViewModels.Cabinet
{
    public class CarViolationViewModel
    {
        [LocalizedDisplayName("ColumnFixationTime", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 100)]
        [JqGridColumnFormatter(JqGridColumnPredefinedFormatters.Date, SourceFormat = "d.m.Y H:i:s", OutputFormat = "d.m.Y H:i")]
        public string FixationTime { get; set; }

        [LocalizedDisplayName("ColumnOrderNumber", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 100)]
        public string OrderNumber { get; set; }

        [LocalizedDisplayName("ColumnPostAddress", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 200)]
        public string PostAddress { get; set; }
       
        [LocalizedDisplayName("ColumnFullName", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 300)]
        [HiddenInput(DisplayValue = false)]
        public string FullName { get; set; }

        [LocalizedDisplayName("CarMark", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        [HiddenInput(DisplayValue = false)]
        public string Mark { get; set; }

        [LocalizedDisplayName("ColumnStatus", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 190)]
        public string Status { get; set; }

        [LocalizedDisplayName("ColumnViolationType", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        public string ViolationType { get; set; }

        [LocalizedDisplayName("ColumnCost", NameResourceType = typeof(CabinetViewModelsStrings))]
        [JqGridColumnSearchable(true, SearchType = JqGridColumnSearchTypes.Text, SearchOperators = JqGridSearchOperators.Cn)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 80)]
        public string Cost { get; set; }


        //[JqGridColumnFormatter("$.customButton")]
        //[DisplayName("")]
        //[JqGridColumnSearchable(false)]
        //[JqGridColumnSortable(false)]
        //[JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 20)]
        //public string Button { get; set; }

        [HiddenInput]
        public bool IsPayed
        {
            get
            {
                return Status.ToLower() == "оплачено" || Status.ToLower() == "прекращено" || Status.ToLower() == "оплата подтверждена";
            }
        }

    }
}
