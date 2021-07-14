using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("MasterTEmplate")]
    public partial class MasterTemplate
    {
        public MasterTemplate()
        {
            DataMetrics = new HashSet<DataMetric>();
            DataTemplates = new HashSet<DataTemplate>();
        }

        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Column("Customer_SID")]
        public int CustomerSid { get; set; }
        [Required]
        [Column("Plant_Template_Code")]
        [StringLength(5)]
        public string PlantTemplateCode { get; set; }
        [Required]
        [Column("Section_Template_Code")]
        [StringLength(5)]
        public string SectionTemplateCode { get; set; }
        [Column("Converter_1_Template_code")]
        [StringLength(5)]
        public string Converter1TemplateCode { get; set; }
        [Column("Converter_2_Template_code")]
        [StringLength(5)]
        public string Converter2TemplateCode { get; set; }
        [Column("Converter_3_Template_code")]
        [StringLength(5)]
        public string Converter3TemplateCode { get; set; }
        [Column("Converter_4_Template_code")]
        [StringLength(5)]
        public string Converter4TemplateCode { get; set; }
        [Required]
        [Column("Site_Site_Id")]
        [StringLength(10)]
        public string SiteSiteId { get; set; }
        [Required]
        [Column("Plant_Plant_SID")]
        [StringLength(10)]
        public string PlantPlantSid { get; set; }

        [ForeignKey(nameof(CustomerSid))]
        [InverseProperty(nameof(Customer.MasterTemplates))]
        public virtual Customer CustomerS { get; set; }
        [ForeignKey(nameof(PlantPlantSid))]
        [InverseProperty(nameof(Plant.MasterTemplates))]
        public virtual Plant PlantPlantS { get; set; }
        [ForeignKey(nameof(SiteSiteId))]
        [InverseProperty(nameof(Site.MasterTemplates))]
        public virtual Site SiteSite { get; set; }
        [InverseProperty(nameof(DataMetric.MasterTemplateS))]
        public virtual ICollection<DataMetric> DataMetrics { get; set; }
        [InverseProperty(nameof(DataTemplate.MasterTemplateS))]
        public virtual ICollection<DataTemplate> DataTemplates { get; set; }
    }
}
