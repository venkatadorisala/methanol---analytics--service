using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("SectionKPI")]
    public partial class SectionKpi
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Required]
        [Column("Converter_Converter_SID")]
        [StringLength(100)]
        public string ConverterConverterSid { get; set; }
        [Column("KPI_Name")]
        [StringLength(100)]
        public string KpiName { get; set; }
        [Column("VARIABLE_NAME")]
        [StringLength(50)]
        public string VariableName { get; set; }
        [Column("ANALYSIS")]
        [StringLength(50)]
        public string Analysis { get; set; }
        [Column("Methanol_Prod_actual")]
        public double? MethanolProdActual { get; set; }
        [Column("Methanol_Prod_actual_unit")]
        [StringLength(30)]
        public string MethanolProdActualUnit { get; set; }
        [Column("Methanol_Prod_actual_perc")]
        public double? MethanolProdActualPerc { get; set; }
        [Column("Methanol_Prod_Total_prod")]
        public double? MethanolProdTotalProd { get; set; }
    }
}
