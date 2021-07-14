using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("PlantKPI")]
    public partial class PlantKpi
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Column("KPI_Name")]
        [StringLength(100)]
        public string KpiName { get; set; }
        [Column("SYNGAS_FLOW_PERCENT")]
        public double? SyngasFlowPercent { get; set; }
        [Column("SYNGAS_FLOW_PERCENT_UNIT")]
        [StringLength(5)]
        public string SyngasFlowPercentUnit { get; set; }
        [Column("SYNGAS_FLOW_ACT_VALUE")]
        public double? SyngasFlowActValue { get; set; }
        [Column("SYNGAS_FLOW_ACT_VALUE_UNIT")]
        [StringLength(30)]
        public string SyngasFlowActValueUnit { get; set; }
        [Column("SYNGAS_FLOW_TOTAL")]
        public double? SyngasFlowTotal { get; set; }
        [Column("SYNGAS_FLOW_TOTAL_UNIT")]
        [StringLength(30)]
        public string SyngasFlowTotalUnit { get; set; }
        [Required]
        [Column("VARIABLE_NAME")]
        [StringLength(50)]
        public string VariableName { get; set; }
        [Column("CONV_PEAK_TEMP_ANALYSIS")]
        [StringLength(50)]
        public string ConvPeakTempAnalysis { get; set; }
        [Column("CONV_PEAK_TEMP")]
        public double? ConvPeakTemp { get; set; }
        [Column("CONV_PEAK_TEMP_UNIT")]
        [StringLength(30)]
        public string ConvPeakTempUnit { get; set; }
        [Column("CONV_PEAK_TEMP_TRGT_MAX")]
        public double? ConvPeakTempTrgtMax { get; set; }
        [Column("CONV_PEAK_TEMP_TRGT_MAX_INCR")]
        public double? ConvPeakTempTrgtMaxIncr { get; set; }
        [Column("CONV_PEAK_TEMP_TRGT_MAX_DECR")]
        public double? ConvPeakTempTrgtMaxDecr { get; set; }
        [Column("CONV_PEAK_TEMP_SFTY_MAX")]
        public double? ConvPeakTempSftyMax { get; set; }
        [Column("CONV_PEAK_TEMP_SFTY_MAX_INCR")]
        public double? ConvPeakTempSftyMaxIncr { get; set; }
        [Column("CONV_PEAK_TEMP_SFTY_MAX_DECR")]
        public double? ConvPeakTempSftyMaxDecr { get; set; }
        [Required]
        [Column("Converter_Converter_SID")]
        [StringLength(10)]
        public string ConverterConverterSid { get; set; }

        [ForeignKey(nameof(ConverterConverterSid))]
        [InverseProperty(nameof(Converter.PlantKpis))]
        public virtual Converter ConverterConverterS { get; set; }
    }
}
