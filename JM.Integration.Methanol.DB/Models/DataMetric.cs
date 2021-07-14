using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    public partial class DataMetric
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Key]
        [Column("Plant_SID")]
        [StringLength(10)]
        public string PlantSid { get; set; }
        [Column("Master_Template_SID")]
        public int MasterTemplateSid { get; set; }
        [StringLength(50)]
        public string Variable { get; set; }
        [Column("Recorded_Unit")]
        [StringLength(30)]
        public string RecordedUnit { get; set; }
        [Column("Value_SU")]
        public double? ValueSu { get; set; }
        [Column("Standard_Unit")]
        [StringLength(30)]
        public string StandardUnit { get; set; }
        [Column("DOU", TypeName = "datetime")]
        public DateTime? Dou { get; set; }
        [Column("Valid_From", TypeName = "datetime")]
        public DateTime? ValidFrom { get; set; }
        [Column("Valid_To", TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }
        [Column("DOR", TypeName = "datetime")]
        public DateTime? Dor { get; set; }
        [Column("Value_PU")]
        public double? ValuePu { get; set; }

        [ForeignKey(nameof(MasterTemplateSid))]
        [InverseProperty(nameof(MasterTemplate.DataMetrics))]
        public virtual MasterTemplate MasterTemplateS { get; set; }
        [ForeignKey(nameof(PlantSid))]
        [InverseProperty(nameof(Plant.DataMetrics))]
        public virtual Plant PlantS { get; set; }
    }
}
