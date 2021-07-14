using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("Section")]
    public partial class Section
    {
        public Section()
        {
            Converters = new HashSet<Converter>();
        }

        [Key]
        [Column("Section_SID")]
        [StringLength(10)]
        public string SectionSid { get; set; }
        [Required]
        [Column("Plant_Plant_SID")]
        [StringLength(10)]
        public string PlantPlantSid { get; set; }
        [Column("Section_Name")]
        [StringLength(100)]
        public string SectionName { get; set; }

        [ForeignKey(nameof(PlantPlantSid))]
        [InverseProperty(nameof(Plant.Sections))]
        public virtual Plant PlantPlantS { get; set; }
        [InverseProperty(nameof(Converter.SectionSectionS))]
        public virtual ICollection<Converter> Converters { get; set; }
    }
}
