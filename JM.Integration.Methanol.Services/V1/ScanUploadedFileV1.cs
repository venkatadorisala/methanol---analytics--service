using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services.Constants;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace JM.Integration.Methanol.Services
{
    /// <summary>
    /// Scans uploaded file using nClam
    /// If file is clean then add new message in Pre-chk-Queue
    /// otherwise delete the file from blob storage(Raw-data-storage)
    /// </summary>
    public class ScanUploadedFileV1
    {
        private readonly IBlobService _blobService;

        private readonly IScanFileAsync _scanFileAsync;

        private readonly IProcessDetailService _processDetailService;

        /// <summary>
        /// Initializes new instance of the<see cref="ScanUploadedFileV1" />class
        /// </summary>
        /// <param name="blobService"></param>
        /// <param name="scanFileAsync"></param>
        public ScanUploadedFileV1(IBlobService blobService, IScanFileAsync scanFileAsync, IProcessDetailService processDetailService)
        {
            _blobService = blobService;
            _scanFileAsync = scanFileAsync;
            _processDetailService = processDetailService;
        }

        /// <summary>
        /// Scans uploaded file 
        /// </summary>
        /// <param name="queueItem"></param>
        /// <param name="log"></param>
        [FunctionName("VirusScanTriggerV1")]
        public void Run([QueueTrigger("pre-av-chk-queue", Connection = "StorageAccountConnectionString")] string queueItem, ILogger log)
        {
            log.LogInformation(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.ProcessStarted, DateTime.Now));
            ProcessDetail uploadedFileProcessDetail = null;

            try
            {
                log.LogInformation(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.ReadFileFromBlob));

                string blobName = _blobService.GetBlobNameFromQueue(queueItem);

                uploadedFileProcessDetail = _processDetailService.GetByFileName(blobName);

                if (uploadedFileProcessDetail != null)
                {
                    var fileContent = _blobService.GetBlobStream(blobName);

                    log.LogInformation(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.FileScanInProgress, blobName));

                    var scanresult = _scanFileAsync.ScanAntvirusCheckAsync(fileContent).Result;

                    log.LogInformation(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.FileScanCompleted));

                    _scanFileAsync.ProcessClamScanResult(scanresult, uploadedFileProcessDetail, blobName, log);
                }
                else
                {
                    log.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.NoMatchingProcessRecord, blobName));
                }
            }
            catch (Exception ex)
            {
                log.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.ErrorInFileScanning, ex.Message));

                if (uploadedFileProcessDetail != null)
                {
                    uploadedFileProcessDetail.AvScanStatus = MethanolConstants.avScanFailed;
                    uploadedFileProcessDetail.ProcessStatus = MethanolConstants.processStatusFailed;
                    uploadedFileProcessDetail.Summary = MethanolConstants.systemErrorSummary;
                    _processDetailService.Update(uploadedFileProcessDetail);
                }
            }

            log.LogInformation(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.ProcessCompleted, DateTime.Now));

        }

    }
}
