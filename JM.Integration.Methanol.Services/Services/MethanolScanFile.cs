using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.Extensions;
using JM.Integration.Methanol.Services.Constants;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using Microsoft.Extensions.Logging;
using nClam;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JM.Integration.Methanol.Services.Services
{

    /// <inheritdoc cref="IScanFileAsync"/>
    public class MethanolScanFile : IScanFileAsync
    {
        private IClamClient _clamavClient;
        private readonly IBlobService _blobService;
        private readonly IStorageQueue _storageQueue;
        private readonly IProcessDetailService _processDetailService;
        public MethanolScanFile(IClamClient clamavClient,
            IBlobService blobService,
            IStorageQueue storageQueue, IProcessDetailService processDetailService)
        {
            _clamavClient = clamavClient;
            _blobService = blobService;
            _storageQueue = storageQueue;
            _processDetailService = processDetailService;

        }
        public async Task<ClamScanResult> ScanAntvirusCheckAsync(Stream fileContent)
        {
            var scanresult = await _clamavClient.SendAndScanFileAsync(fileContent);

            return scanresult;
        }

        public bool ProcessClamScanResult(ClamScanResult clamScanResults,ProcessDetail processDetail ,string blobName, ILogger logger)
        {
            var isStatusUpdated = false;
            if (clamScanResults == null) return false;

            var connectionString = Environment.GetEnvironmentVariable("StorageAccountConnectionString");

            switch (clamScanResults.Result)
            {
                case ClamScanResults.Clean:

                    logger.LogInformation(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.FileClean));

                    var precheckqueue = Environment.GetEnvironmentVariable("pre-chk-queue");

                    UpdateScanStatus(Common.Enums.ClamScanResult.Clean, processDetail, blobName, logger);

                    _storageQueue.AddMessageToStorageQueue(connectionString, precheckqueue, blobName);

                    isStatusUpdated = true;

                    break;

                case ClamScanResults.VirusDetected:

                    logger.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.VirusDected, clamScanResults.InfectedFiles.FirstOrDefault().VirusName));

                    _blobService.DeleteBlob(blobName);

                    UpdateScanStatus(Common.Enums.ClamScanResult.VirusDetected, processDetail, blobName, logger);

                    isStatusUpdated = true;

                    break;

                case ClamScanResults.Error:

                    logger.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.ScanErrorResult, clamScanResults.RawResult));

                    UpdateScanStatus(Common.Enums.ClamScanResult.Error, processDetail, blobName, logger);

                    isStatusUpdated = true;

                    break;

                case ClamScanResults.Unknown:

                    logger.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.ScanUnknownResult, clamScanResults.RawResult));

                    UpdateScanStatus(Common.Enums.ClamScanResult.Unknown, processDetail, blobName, logger);

                    isStatusUpdated = true;

                    break;
            }

            return isStatusUpdated;

        }

        public bool UpdateScanStatus(Common.Enums.ClamScanResult clamScanResult, ProcessDetail processDetail , string blobName, ILogger logger)
        {

            var isScanStatusUpdated = false;

            if (processDetail != null)
            {
                processDetail.AvScanStatus = EnumExtensionMethods.GetEnumDescription(clamScanResult);

                switch (clamScanResult)
                {
                    case Common.Enums.ClamScanResult.Clean:
                        processDetail.ProcessStatus = null;
                        processDetail.Summary = null;
                        logger.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.FileReadyForPreCheck, processDetail.Sid));
                        break;
                    case Common.Enums.ClamScanResult.VirusDetected:
                        processDetail.ProcessStatus = MethanolConstants.processStatusFailed;
                        processDetail.Summary = FileScanningConstants.infectedFileErrorSummary;
                        break;
                    default:
                        processDetail.ProcessStatus = MethanolConstants.processStatusFailed;
                        processDetail.Summary = MethanolConstants.systemErrorSummary;
                        logger.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.FileFailedForPreCheck, processDetail.Sid));
                        break;
                }

                isScanStatusUpdated = _processDetailService.Update(processDetail);
            }
            else
            {
                logger.LogError(string.Format(CultureInfo.InvariantCulture, FileScanningConstants.NoMatchingProcessRecord, blobName));
            }

            return isScanStatusUpdated;

        }

    }
}
