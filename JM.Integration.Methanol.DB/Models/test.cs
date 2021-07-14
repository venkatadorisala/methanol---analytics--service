using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JM.Integration.Methanol.DB.Models
{
   public class Test
    {
        [Key]
        [Column("Converter_SID")]
        [StringLength(10)]
        public string ConverterSid { get; set; }
    }
}
