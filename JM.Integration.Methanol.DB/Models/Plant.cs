using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("Plant")]
    public partial class Plant
    {
        public Plant()
        {
            DataMetrics = new HashSet<DataMetric>();
            FittingMetrics = new HashSet<FittingMetric>();
            MasterTemplates = new HashSet<MasterTemplate>();
            PlantProvisionalUnits = new HashSet<PlantProvisionalUnit>();
            ProcessDetails = new HashSet<ProcessDetail>();
            Sections = new HashSet<Section>();
        }

        [Key]
        [Column("Plant_SID")]
        [StringLength(10)]
        public string PlantSid { get; set; }
        [Required]
        [Column("Site_Site_Id")]
        [StringLength(10)]
        public string SiteSiteId { get; set; }
        [Column("Plant_Name")]
        [StringLength(100)]
        public string PlantName { get; set; }
        [Column("Date_of_establish", TypeName = "datetime")]
        public DateTime? DateOfEstablish { get; set; }
        [Column("Plant_Capacity")]
        public double? PlantCapacity { get; set; }
        [Column("Plant_capacity_unit")]
        [StringLength(30)]
        public string PlantCapacityUnit { get; set; }
        [Column("Plant_Status")]
        [StringLength(20)]
        public string PlantStatus { get; set; }
        [Column("Max_Prod_flow")]
        public double? MaxProdFlow { get; set; }
        [Column("Max_Prod_flow_unit")]
        [StringLength(30)]
        public string MaxProdFlowUnit { get; set; }
        [Column("Max_Syngas_flow")]
        public double? MaxSyngasFlow { get; set; }
        [Column("Max_Syng_flow_unit")]
        [StringLength(30)]
        public string MaxSyngFlowUnit { get; set; }
        [Column("R_Ratio_Target")]
        public double? RRatioTarget { get; set; }
        [Column("Plant_Molecular_wt")]
        public double? PlantMolecularWt { get; set; }

        [InverseProperty(nameof(DataMetric.PlantS))]
        public virtual ICollection<DataMetric> DataMetrics { get; set; }
        [InverseProperty(nameof(FittingMetric.PlantS))]
        public virtual ICollection<FittingMetric> FittingMetrics { get; set; }
        [InverseProperty(nameof(MasterTemplate.PlantPlantS))]
        public virtual ICollection<MasterTemplate> MasterTemplates { get; set; }
        [InverseProperty(nameof(PlantProvisionalUnit.PlantPlantS))]
        public virtual ICollection<PlantProvisionalUnit> PlantProvisionalUnits { get; set; }
        [InverseProperty(nameof(ProcessDetail.PlantPlantS))]
        public virtual ICollection<ProcessDetail> ProcessDetails { get; set; }
        [InverseProperty(nameof(Section.PlantPlantS))]
        public virtual ICollection<Section> Sections { get; set; }
    }
}
