using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("ModelPlantMapping")]
    public partial class ModelPlantMapping
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Column("Model_Variable_SID")]
        public int ModelVariableSid { get; set; }
        [Column("Data_Template_SID")]
        public int? DataTemplateSid { get; set; }
        [Column("Mapping_Type")]
        [StringLength(20)]
        public string MappingType { get; set; }
        [StringLength(50)]
        public string Step { get; set; }
        [StringLength(50)]
        public string Weight { get; set; }

        [ForeignKey(nameof(DataTemplateSid))]
        [InverseProperty(nameof(DataTemplate.ModelPlantMappings))]
        public virtual DataTemplate DataTemplateS { get; set; }
        [ForeignKey(nameof(ModelVariableSid))]
        [InverseProperty(nameof(ModelVariable.ModelPlantMappings))]
        public virtual ModelVariable ModelVariableS { get; set; }
    }
}
