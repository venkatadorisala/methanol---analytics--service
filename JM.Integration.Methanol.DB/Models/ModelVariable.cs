using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    public partial class ModelVariable
    {
        public ModelVariable()
        {
            FittingMetrics = new HashSet<FittingMetric>();
            ModelPlantMappings = new HashSet<ModelPlantMapping>();
        }

        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Column("MDM_SID")]
        public int MdmSid { get; set; }
        [Required]
        [Column("Variable_Name")]
        [StringLength(50)]
        public string VariableName { get; set; }
        [StringLength(30)]
        public string Unit { get; set; }
        [Column("Current_Value")]
        public double? CurrentValue { get; set; }
        [Column("Read_Write")]
        [StringLength(5)]
        public string ReadWrite { get; set; }
        [Column("Model_Name")]
        [StringLength(50)]
        public string ModelName { get; set; }
        [StringLength(100)]
        public string Section { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Step { get; set; }
        [StringLength(100)]
        public string Weight { get; set; }
        [Column("Create_date", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Column("Create_by")]
        [StringLength(100)]
        public string CreateBy { get; set; }
        [Column("Update_date", TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column("Updated_by")]
        [StringLength(100)]
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(MdmSid))]
        [InverseProperty(nameof(ModelDefinition.ModelVariables))]
        public virtual ModelDefinition MdmS { get; set; }
        [InverseProperty(nameof(FittingMetric.MvS))]
        public virtual ICollection<FittingMetric> FittingMetrics { get; set; }
        [InverseProperty(nameof(ModelPlantMapping.ModelVariableS))]
        public virtual ICollection<ModelPlantMapping> ModelPlantMappings { get; set; }
    }
}
