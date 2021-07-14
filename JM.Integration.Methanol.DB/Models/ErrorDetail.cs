using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    public partial class ErrorDetail
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Column("Process_Details_SID")]
        public int ProcessDetailsSid { get; set; }
        [Column("File_Name")]
        [StringLength(100)]
        public string FileName { get; set; }
        [Column("Rec_Number")]
        public int? RecNumber { get; set; }
        [Column("DOReading", TypeName = "datetime")]
        public DateTime? Doreading { get; set; }
        [Column("DOUpdate", TypeName = "datetime")]
        public DateTime? Doupdate { get; set; }
        [Column("Error_Desc")]
        [StringLength(4000)]
        public string ErrorDesc { get; set; }
        [Column("Error_Rec_Status")]
        [StringLength(40)]
        public string ErrorRecStatus { get; set; }

        [ForeignKey(nameof(ProcessDetailsSid))]
        [InverseProperty(nameof(ProcessDetail.ErrorDetails))]
        public virtual ProcessDetail ProcessDetailsS { get; set; }
    }
}
