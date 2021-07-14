using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("Converter")]
    public partial class Converter
    {
        public Converter()
        {
            ConverterKpis = new HashSet<ConverterKpi>();
            PlantKpis = new HashSet<PlantKpi>();
        }

        [Key]
        [Column("Converter_SID")]
        [StringLength(10)]
        public string ConverterSid { get; set; }
        [Required]
        [Column("Section_Section_SID")]
        [StringLength(10)]
        public string SectionSectionSid { get; set; }
        [Column("Converter_Name")]
        [StringLength(100)]
        public string ConverterName { get; set; }
        [Column("Date_of_establish", TypeName = "date")]
        public DateTime? DateOfEstablish { get; set; }
        [Column("Reactor_Trgt_max_temp")]
        public double? ReactorTrgtMaxTemp { get; set; }
        [Column("Reactor_Trgt_max_temp_unit")]
        [StringLength(30)]
        public string ReactorTrgtMaxTempUnit { get; set; }
        [Column("Reactor_Safe_max_temp")]
        public double? ReactorSafeMaxTemp { get; set; }
        [Column("Reactor_Safe_max_temp_unit")]
        [StringLength(30)]
        public string ReactorSafeMaxTempUnit { get; set; }
        [Column("Reactor_Ref_flow_rate")]
        public double? ReactorRefFlowRate { get; set; }
        [Column("Reactor_Ref_flow_rate_unit")]
        [StringLength(30)]
        public string ReactorRefFlowRateUnit { get; set; }
        [Column("Reactor_duty")]
        [StringLength(30)]
        public string ReactorDuty { get; set; }
        [Column("Reactor_type")]
        [StringLength(30)]
        public string ReactorType { get; set; }
        [Column("Catalyst_name")]
        [StringLength(50)]
        public string CatalystName { get; set; }
        [Column("Catalyst_supplier")]
        [StringLength(50)]
        public string CatalystSupplier { get; set; }
        [Column("Catalyst_volume")]
        public double? CatalystVolume { get; set; }
        [Column("Catalyst_volume_unit")]
        [StringLength(30)]
        public string CatalystVolumeUnit { get; set; }
        [Column("Catalyst_start date", TypeName = "date")]
        public DateTime? CatalystStartDate { get; set; }
        [Column("Catalyst_exp_chg_date", TypeName = "date")]
        public DateTime? CatalystExpChgDate { get; set; }
        [Column("Reactor_AET_type")]
        public double? ReactorAetType { get; set; }
        [Column("Reactor_AET_type_unit")]
        [StringLength(30)]
        public string ReactorAetTypeUnit { get; set; }
        [Column("Reactor_safe_AET_Temp")]
        public double? ReactorSafeAetTemp { get; set; }
        [Column("Reactor_safe_AET_Temp_unit")]
        [StringLength(30)]
        public string ReactorSafeAetTempUnit { get; set; }
        [Column("Converter_Status")]
        [StringLength(20)]
        public string ConverterStatus { get; set; }

        [ForeignKey(nameof(SectionSectionSid))]
        [InverseProperty(nameof(Section.Converters))]
        public virtual Section SectionSectionS { get; set; }
        [InverseProperty(nameof(ConverterKpi.ConverterConverterS))]
        public virtual ICollection<ConverterKpi> ConverterKpis { get; set; }
        [InverseProperty(nameof(PlantKpi.ConverterConverterS))]
        public virtual ICollection<PlantKpi> PlantKpis { get; set; }
    }
}
