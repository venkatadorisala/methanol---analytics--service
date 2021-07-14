using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            MasterTemplates = new HashSet<MasterTemplate>();
        }

        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Column("Customer_Name")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [InverseProperty(nameof(MasterTemplate.CustomerS))]
        public virtual ICollection<MasterTemplate> MasterTemplates { get; set; }
    }
}
