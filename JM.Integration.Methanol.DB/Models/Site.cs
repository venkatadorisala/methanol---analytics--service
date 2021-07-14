using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("Site")]
    public partial class Site
    {
        public Site()
        {
            MasterTemplates = new HashSet<MasterTemplate>();
        }

        [Key]
        [Column("Site_Id")]
        [StringLength(10)]
        public string SiteId { get; set; }
        [Column("Site_Name")]
        [StringLength(100)]
        public string SiteName { get; set; }
        [Column("Date_of_establish", TypeName = "date")]
        public DateTime? DateOfEstablish { get; set; }
        [Column("Company_Name")]
        [StringLength(100)]
        public string CompanyName { get; set; }
        [Column("Site_location_state")]
        [StringLength(30)]
        public string SiteLocationState { get; set; }
        [Column("Site_location_country")]
        [StringLength(30)]
        public string SiteLocationCountry { get; set; }
        [Column("Site_Subsr_tier")]
        [StringLength(30)]
        public string SiteSubsrTier { get; set; }
        [Column("Customer_SID")]
        public int CustomerSid { get; set; }

        [InverseProperty(nameof(MasterTemplate.SiteSite))]
        public virtual ICollection<MasterTemplate> MasterTemplates { get; set; }
    }
}
