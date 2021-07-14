using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    public partial class ProcessDetail
    {
        public ProcessDetail()
        {
            ErrorDetails = new HashSet<ErrorDetail>();
        }

        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Required]
        [Column("Plant_Plant_SID")]
        [StringLength(10)]
        public string PlantPlantSid { get; set; }
        [Column("Site_Id")]
        [StringLength(10)]
        public string SiteId { get; set; }
        [Column("Upload_File_Name")]
        [StringLength(100)]
        public string UploadFileName { get; set; }
        [Column("Upload_Date", TypeName = "datetime")]
        public DateTime? UploadDate { get; set; }
        [Column("Upload_File_Status")]
        [StringLength(30)]
        public string UploadFileStatus { get; set; }
        [Column("Reading_Date", TypeName = "date")]
        public DateTime? ReadingDate { get; set; }
        [Column("AV_Scan_Status")]
        [StringLength(30)]
        public string AvScanStatus { get; set; }
        [Column("Pre_Chk_Status")]
        [StringLength(120)]
        public string PreChkStatus { get; set; }
        [Column("History_Rev_Flag")]
        [StringLength(5)]
        public string HistoryRevFlag { get; set; }
        [Column("Process_Date", TypeName = "datetime")]
        public DateTime? ProcessDate { get; set; }
        [Column("Process_Status")]
        [StringLength(30)]
        public string ProcessStatus { get; set; }
        [StringLength(200)]
        public string Summary { get; set; }
        [StringLength(50)]
        public string Report { get; set; }

        [ForeignKey(nameof(PlantPlantSid))]
        [InverseProperty(nameof(Plant.ProcessDetails))]
        public virtual Plant PlantPlantS { get; set; }
        [InverseProperty(nameof(ErrorDetail.ProcessDetailsS))]
        public virtual ICollection<ErrorDetail> ErrorDetails { get; set; }
    }
}
