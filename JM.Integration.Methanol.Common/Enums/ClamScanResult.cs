using JM.Integration.Methanol.Common.Constants;
using System.ComponentModel;

namespace JM.Integration.Methanol.Common.Enums
{
    /// <summary>
    /// Defines all the clam AV scan result status
    /// </summary>
    public enum ClamScanResult
    {
        /// <summary>
        /// Unknown faiures
        /// </summary>
        [Description(ClamScanConstants.failedInAvScan)]
        Unknown = 0,

        /// <summary>
        /// Clean file
        /// </summary>
        [Description(ClamScanConstants.Clean)]
        Clean = 1,

        /// <summary>
        /// Infected file due to Virus
        /// </summary>
        [Description(ClamScanConstants.failedInAvScan)]
        VirusDetected = 2,

        /// <summary>
        /// Error in scanning
        /// </summary>
        [Description(ClamScanConstants.failedInAvScan)]
        Error = 3
    }

}
