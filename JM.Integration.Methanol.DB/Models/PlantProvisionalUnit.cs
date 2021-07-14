using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    public partial class PlantProvisionalUnit
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Required]
        [Column("Plant_Plant_SID")]
        [StringLength(10)]
        public string PlantPlantSid { get; set; }
        [Column("UOM_UNIT_SID")]
        public int UomUnitSid { get; set; }
        [Column("Provisional_Unit_Name")]
        [StringLength(50)]
        public string ProvisionalUnitName { get; set; }
        [Column("Provisional_Unit_symbol")]
        [StringLength(30)]
        public string ProvisionalUnitSymbol { get; set; }

        [ForeignKey(nameof(PlantPlantSid))]
        [InverseProperty(nameof(Plant.PlantProvisionalUnits))]
        public virtual Plant PlantPlantS { get; set; }
        [ForeignKey(nameof(UomUnitSid))]
        [InverseProperty(nameof(UnitsofMeasurement.PlantProvisionalUnits))]
        public virtual UnitsofMeasurement UomUnitS { get; set; }
    }
}
