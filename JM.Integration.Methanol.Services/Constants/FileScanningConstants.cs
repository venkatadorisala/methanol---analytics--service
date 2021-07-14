namespace JM.Integration.Methanol.Services.Constants
{
    /// <summary>
    /// Defines list of file scanning process constants
    /// </summary>
    public static class FileScanningConstants
    {
        /// <summary>
        /// ProcessStarted.
        /// </summary>
        public const string ProcessStarted = "File scanning process started at {0}.";

        /// <summary>
        /// ReadFileFromBlob.
        /// </summary>
        public const string ReadFileFromBlob = "Reading file from blob and getting current processing record from process details table.";

        /// <summary>
        /// ValidationCompleted.
        /// </summary>
        public const string FileScanInProgress = "File {0} antivirus scanning is in progress.";

        /// <summary>
        /// ProcessCompleted.
        /// </summary>
        public const string FileScanCompleted = "File scanning completed , adding message in queue and update status in database.";

        /// <summary>
        /// FileClean.
        /// </summary>
        public const string FileClean = "The file is clean and ready for preliminary check hence adding filename into pre-chk-queue.";

        /// <summary>
        /// VirusDected.
        /// </summary>
        public const string VirusDected = "Virus Found! Virus name: {0}.";

        /// <summary>
        /// ScanErrorResult.
        /// </summary>
        public const string ScanErrorResult = "An error occured while scaning the file! ScanResult: {0}.";

        /// <summary>
        /// ScanUnknownResult.
        /// </summary>
        public const string ScanUnknownResult = "Unknown scan result while scaning the file!ScanResult: {0}.";

        /// <summary>
        /// FileReadyForPreCheck.
        /// </summary>
        public const string FileReadyForPreCheck = "File scanning successfully completed and updated to process details table SID={0} .The file is now ready for preliminary check.";

        /// <summary>
        /// FileFailedForPreCheck.
        /// </summary>
        public const string FileFailedForPreCheck = "File scanning failed and updated to process details table SID={0}.The file is not ready for preliminary check.Please review the log.";

        /// <summary>
        /// NoMatchingProcessRecord
        /// </summary>
        public const string NoMatchingProcessRecord = "File is clean but not able to update status in process details table due to no macthing record found in process details for filename {0}.";

        /// <summary>
        /// ErrorInFileScanning
        /// </summary>
        public const string ErrorInFileScanning = "Issue occured in uploaded file scanning process : {0}.";

        /// <summary>
        /// ProcessCompleted.
        /// </summary>
        public const string ProcessCompleted = "File scanning process completed at {0}.";

        /// <summary>
        /// Clam scan negative test case data(standard Eicar test data)
        /// </summary>
        public const string negativeTestData = @"X5O!P%@AP[4\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*";

        /// <summary>
        /// Infected file error summary to user
        /// </summary>
        public const string infectedFileErrorSummary = "A potential virus has been detected the file can not be uploaded";



    }
}