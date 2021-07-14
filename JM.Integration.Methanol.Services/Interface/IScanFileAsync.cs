using JM.Integration.Methanol.DB.Models;
using Microsoft.Extensions.Logging;
using nClam;
using System.IO;
using System.Threading.Tasks;

namespace JM.Integration.Methanol.Services.Interface
{
    /// <summary>
    /// Contains methods for file scanning implementation
    /// </summary>
    public interface IScanFileAsync
    {

        /// <summary>
        /// Calls Clam AV service from nClam for scanning file content
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns>Returns the file scanning status</returns>
        Task<ClamScanResult> ScanAntvirusCheckAsync(Stream fileContent);

        /// <summary>
        /// Processing the scanned result from file scanning results
        /// </summary>
        /// <param name="clamScanResults"></param>
        /// <param name="blobName"></param>
        /// <param name="logger"></param>
        /// <returns>Returs updated status</returns>
        bool ProcessClamScanResult(ClamScanResult clamScanResults, ProcessDetail processDetail, string blobName, ILogger logger);

        /// <summary>
        /// Update scan status in process details av_scan_status
        /// Add the messsage in Pre-Chk-queue for next process
        /// </summary>
        /// <param name="clamScanResult"></param>
        /// <param name="blobName"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        bool UpdateScanStatus(Common.Enums.ClamScanResult clamScanResult, ProcessDetail processDetail, string blobName, ILogger logger);

    }
}