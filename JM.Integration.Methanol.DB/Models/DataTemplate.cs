using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("DataTemplate")]
    public partial class DataTemplate
    {
        public DataTemplate()
        {
            ModelPlantMappings = new HashSet<ModelPlantMapping>();
        }

        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Required]
        [Column("Template_Code")]
        [StringLength(5)]
        public string TemplateCode { get; set; }
        [Required]
        [Column("Template_Type")]
        [StringLength(20)]
        public string TemplateType { get; set; }
        [Column("Template_Version")]
        [StringLength(5)]
        public string TemplateVersion { get; set; }
        [Required]
        [StringLength(100)]
        public string Section { get; set; }
        [Column("Sub_Section")]
        [StringLength(100)]
        public string SubSection { get; set; }
        [Required]
        [StringLength(50)]
        public string Variable { get; set; }
        [Column("Column_no")]
        public int ColumnNo { get; set; }
        [Column("UOM_SID")]
        public int UomSid { get; set; }
        [Column("Range_From")]
        public double? RangeFrom { get; set; }
        [Column("Range_To")]
        public double? RangeTo { get; set; }
        public bool? Mandatory { get; set; }
        [Column("Create_date", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Column("Update_date", TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column("Created_By")]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Column("Updated_by")]
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        [Column("Master_Template_SID")]
        public int MasterTemplateSid { get; set; }
        [Column("Provisional_unit")]
        [StringLength(30)]
        public string ProvisionalUnit { get; set; }

        [ForeignKey(nameof(MasterTemplateSid))]
        [InverseProperty(nameof(MasterTemplate.DataTemplates))]
        public virtual MasterTemplate MasterTemplateS { get; set; }
        [ForeignKey(nameof(UomSid))]
        [InverseProperty(nameof(UnitsofMeasurement.DataTemplates))]
        public virtual UnitsofMeasurement UomS { get; set; }
        [InverseProperty(nameof(ModelPlantMapping.DataTemplateS))]
        public virtual ICollection<ModelPlantMapping> ModelPlantMappings { get; set; }
    }
}
