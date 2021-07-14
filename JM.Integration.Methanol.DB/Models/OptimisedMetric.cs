using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    [Table("OptimisedMetric")]
    public partial class OptimisedMetric
    {
        [Key]
        [Column("SID")]
        public int Sid { get; set; }
        [Column("Current_Value")]
        public double? CurrentValue { get; set; }
        [Column("Measured_Value_SU")]
        public double? MeasuredValueSu { get; set; }
        [Column("Standard_Unit")]
        [StringLength(10)]
        public string StandardUnit { get; set; }
        [Column("Fitted_Value_SU")]
        public double? FittedValueSu { get; set; }
        [Column("Provisional_Unit")]
        [StringLength(10)]
        public string ProvisionalUnit { get; set; }
        [Column("Fitted_Value_PU")]
        public double? FittedValuePu { get; set; }
        public double? Step { get; set; }
        public double? Weight { get; set; }
        [Column("SOS")]
        public double? Sos { get; set; }
        [Column("Standard_Error")]
        public double? StandardError { get; set; }
        public double? Change { get; set; }
        [Column("Change_%")]
        public double? Change1 { get; set; }
        [Column("SOS_%")]
        public double? Sos1 { get; set; }
        [Column("Fitting_Date", TypeName = "date")]
        public DateTime? FittingDate { get; set; }
        [Column("Reading_Date", TypeName = "date")]
        public DateTime? ReadingDate { get; set; }
        [Column("Upload_Date", TypeName = "date")]
        public DateTime? UploadDate { get; set; }
        [Column("Valid_From", TypeName = "datetime")]
        public DateTime? ValidFrom { get; set; }
        [Column("Valid_To", TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }
        [Column("Model_SID")]
        public int ModelSid { get; set; }
        [Column("Model_Variable_SID")]
        public int ModelVariableSid { get; set; }
        [Column("Plant_SID")]
        public int PlantSid { get; set; }
        [Column("Optimised_Value")]
        public double? OptimisedValue { get; set; }
    }
}
