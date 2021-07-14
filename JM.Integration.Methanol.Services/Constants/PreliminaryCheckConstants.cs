namespace JM.Integration.Methanol.Services.Constants
{
   
    /// <summary>
    /// Defines list of preliminary check constants
    /// </summary>
    public static class PreliminaryCheckConstants
    {
        /// <summary>
        /// ProcessStarted.
        /// </summary>
        public const string ProcessStarted = "Preliminary check of file at {0}.";
        /// <summary>
        /// ReadFileFromBlob.
        /// </summary>
        public const string ReadFileFromBlob = "Reading file from blob.";

        /// <summary>
        /// File extension validation.
        /// </summary>

        public const string SupportedFileExtensions = "Extension of uploaded file {0}   is not supported. Supported extensions are XLSX and XLSM";
        /// <summary>
        /// Extracting excel file content
        /// </summary>
        public const string ExtractExcel = "Extracting excel file content";

        public const string PreliminaryCheckSuccessful = "Success in Pre Check";

        public const string IncorrectTemplateVersion = "Incorrect template version, please use the latest template version available from your TSE.";
        public const string FailedWrongTemplateUsed = "Incorrect template for the selected plant, please check the template used and plant selected.";
        public const string PlantDetailsNotAvailabe = "File upload failed. Plant details are not availabe.";
        public const string FileUploadFailed = "Failed";
        public const string BlobUploadFailed = "Blob upload is failed.";

        public const string FailedinPreCheck = "Failed in pre-check";


    }
}
