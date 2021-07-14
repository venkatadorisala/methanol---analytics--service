namespace JM.Integration.Methanol.Services.Constants
{
    /// <summary>
    /// Defines list of common constants for methanol project
    /// </summary>
    public static class MethanolConstants
    {
        /// <summary>
        /// ProcessStarted.
        /// </summary>
        public const string ProcessStarted = "Process Started";

        /// <summary>
        /// HttpResponseMessage.
        /// </summary>
        public const string HttpResponseMessage = "Http Response Status:";

        /// <summary>
        /// ValidationCompleted.
        /// </summary>
        public const string ValidationCompleted = "Validation Completed";

        /// <summary>
        /// ProcessCompleted.
        /// </summary>
        public const string ProcessCompleted = "Process Completed";

        /// <summary>
        /// SubjectGroupInErrorMessage.
        /// </summary>
        public const string InvalidSectionId = "Id not found.";

        /// <summary>
        /// LogMessageFormat.
        /// </summary>
        public const string LogMessageFormat = "MethanolMicroservice: CorrelationIdentifier: {0}, Message: {1}-{2}.";

        /// <summary>
        /// LogExceptionMessageFormat.
        /// </summary>
        public const string LogExceptionMessageFormat = "MethanolMicroservice: CorrelationIdentifier: {0}, Exception Message: {1} - {2}.";

        /// <summary>
        /// System error due to technical exception
        /// </summary>
        public const string avScanFailed = "Failed in AV_Scan";

        /// <summary>
        /// Genric error message due to System error to show user in UI
        /// </summary>
        public const string systemErrorSummary = "A potential system error has been detected file can not be uploaded";

        /// <summary>
        /// Manual upload process failed
        /// </summary>
        public const string processStatusFailed = "Failed";
    }
}