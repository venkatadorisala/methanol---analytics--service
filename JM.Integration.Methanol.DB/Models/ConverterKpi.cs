using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("ConverterKPI")]
    public partial class ConverterKpi
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Required]
        [Column("Converter_Converter_SID")]
        [StringLength(10)]
        public string ConverterConverterSid { get; set; }
        [Column("KPI_Name")]
        [StringLength(100)]
        public string KpiName { get; set; }
        [Required]
        [Column("VARIABLE_NAME")]
        [StringLength(50)]
        public string VariableName { get; set; }
        [Column("ANALYSIS")]
        [StringLength(50)]
        public string Analysis { get; set; }
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
        [Column("CONV_PRESS_DROP_MSD")]
        public double? ConvPressDropMsd { get; set; }
        [Column("CONV_PRESS_DROP_MSD_UNIT")]
        [StringLength(30)]
        public string ConvPressDropMsdUnit { get; set; }
        [Column("CONV_PRESS_DROP_LF_AVG_NRM")]
        public double? ConvPressDropLfAvgNrm { get; set; }
        [Column("CONV_PRESS_DROP_FW_AVG_NRM")]
        public double? ConvPressDropFwAvgNrm { get; set; }
        [Column("CONV_PRESS_DROP_AVG_NRM_UNIT")]
        [StringLength(30)]
        public string ConvPressDropAvgNrmUnit { get; set; }
        [Column("CONV_PRESS_DRP_FW_AVG_NRM_INCR")]
        public double? ConvPressDrpFwAvgNrmIncr { get; set; }
        [Column("CONV_PRESS_DRP_FW_AVG_NRM_DECR")]
        public double? ConvPressDrpFwAvgNrmDecr { get; set; }

        [ForeignKey(nameof(ConverterConverterSid))]
        [InverseProperty(nameof(Converter.ConverterKpis))]
        public virtual Converter ConverterConverterS { get; set; }
    }
}
