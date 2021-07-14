using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("UnitsofMeasurement")]
    public partial class UnitsofMeasurement
    {
        public UnitsofMeasurement()
        {
            DataTemplates = new HashSet<DataTemplate>();
            PlantProvisionalUnits = new HashSet<PlantProvisionalUnit>();
        }

        [Key]
        [Column("UNIT_SID")]
        public int UnitSid { get; set; }
        [Column("Unit_Family")]
        [StringLength(30)]
        public string UnitFamily { get; set; }
        [Column("Unit_Name")]
        [StringLength(30)]
        public string UnitName { get; set; }
        [Column("Unit_Symbol")]
        [StringLength(15)]
        public string UnitSymbol { get; set; }

        [InverseProperty(nameof(DataTemplate.UomS))]
        public virtual ICollection<DataTemplate> DataTemplates { get; set; }
        [InverseProperty(nameof(PlantProvisionalUnit.UomUnitS))]
        public virtual ICollection<PlantProvisionalUnit> PlantProvisionalUnits { get; set; }
    }
}
