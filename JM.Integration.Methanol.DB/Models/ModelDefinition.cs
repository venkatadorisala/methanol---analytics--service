using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("ModelDefinition")]
    public partial class ModelDefinition
    {
        public ModelDefinition()
        {
            ModelVariables = new HashSet<ModelVariable>();
        }

        [Key]
        [Column("Model_SID")]
        public int ModelSid { get; set; }
        [Required]
        [Column("Model_Name")]
        [StringLength(30)]
        public string ModelName { get; set; }

        [InverseProperty(nameof(ModelVariable.MdmS))]
        public virtual ICollection<ModelVariable> ModelVariables { get; set; }
    }
}
