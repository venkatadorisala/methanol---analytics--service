using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services.Constants;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Globalization;


namespace JM.Integration.Methanol.Services
{

    /// <summary>
    /// Reads the message from pre-chk-queue and verifies whether uploaded file is having valid PlantId, Plant Name, and Template Version in the control sheet.
    /// After successful verification file is moved from storage account to data lake.
    /// </summary>
    public class PreliminaryCheckUploadedFileV1
    {
        private readonly IFileExtensionValidationService _fileExtensionValidationService;
        private readonly IExcelFileContentExtractorService _excelFileContentExtractorService;
        private readonly IProcessDetailService _processDetailService;
        private readonly IBlobService _blobService;
        private readonly IPlantService _plantService;

        /// <summary>
        /// Initializes new instance of the<see cref="PreliminaryCheckUploadedFileV1" />class
        /// </summary>
        /// <param name="fileExtensionValidationService"></param>
        /// <param name="excelFileContentExtractorService"></param>
        /// <param name="processDetailService"></param>
        /// <param name="blobService"></param>
        public PreliminaryCheckUploadedFileV1(IFileExtensionValidationService fileExtensionValidationService,
            IExcelFileContentExtractorService excelFileContentExtractorService, IProcessDetailService processDetailService, IBlobService blobService, IPlantService plantService)
        {
            _fileExtensionValidationService = fileExtensionValidationService;
            _excelFileContentExtractorService = excelFileContentExtractorService;
            _processDetailService = processDetailService;
            _blobService = blobService;
            _plantService = plantService;
        }

        /// <summary>
        ///  Polls the pre-chk-queue.
        /// </summary>
        /// <param name="queueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("PreliminaryCheckUploadedFileV1")]
        public async Task Run([QueueTrigger("pre-chk-queue", Connection = "StorageAccountConnectionString")] string queueItem, ILogger log)
        {

            log.LogInformation(string.Format(CultureInfo.InvariantCulture, PreliminaryCheckConstants.ProcessStarted, DateTime.Now));

            try
            {
                var isValidFileExtension = _fileExtensionValidationService.IsValidExtension(queueItem);
                if (isValidFileExtension)
                {
                    string blobName = queueItem;

                    log.LogInformation(string.Format(CultureInfo.InvariantCulture, PreliminaryCheckConstants.ReadFileFromBlob));

                    var filecontent = _blobService.GetBlobStream(blobName);

                    log.LogInformation(string.Format(CultureInfo.InvariantCulture, PreliminaryCheckConstants.ExtractExcel));

                    var excelFileContent = _excelFileContentExtractorService.GetFileContent(filecontent);

                    if (excelFileContent != null)
                    {

                        var plantDetails = _plantService.GetPlantDetails(excelFileContent.PlantName);


                        
                        if (plantDetails != null)
                        {

                            var masterTemplate = (MasterTemplate)null;
                            var processDetails = _processDetailService.GetProcessDetailsByPlantName(excelFileContent.PlantName, blobName);

                            if (processDetails != null)
                            {
                                //If data template version exisists 
                                masterTemplate = _processDetailService.GetMasterTemplateByPlantSid(plantDetails.PlantSid);
                                if (masterTemplate != null && masterTemplate.DataTemplates.Any(x => x.TemplateVersion == excelFileContent.TemplateVersion))
                                {

                                    bool status = await _blobService.UploadBlob(queueItem, _blobService.GetBlobStream(blobName), Environment.GetEnvironmentVariable("afterPrechkBlobContainer")).ConfigureAwait(true); ;
                                    if (status)
                                    {
                                        UpdatePreCheckStatus(processDetails, preChkStatus: PreliminaryCheckConstants.PreliminaryCheckSuccessful); ;
                                    }
                                    else
                                    {
                                        log.LogInformation(string.Format(CultureInfo.InvariantCulture, PreliminaryCheckConstants.BlobUploadFailed));
                                    }



                                }
                                else
                                {
                                    UpdatePreCheckStatus(processDetails, preChkStatus: PreliminaryCheckConstants.FailedinPreCheck, summary: PreliminaryCheckConstants.IncorrectTemplateVersion, processStatus: PreliminaryCheckConstants.FileUploadFailed); ;
                                }
                            }
                            else
                            {
                               //Incorrect template selected.
                                UpdateIncorrectTemplate(blobName);
                            }

                        }
                        else
                        {
                            //Plant details not available in plant table.

                            UpdateIncorrectTemplate(blobName);
                            log.LogInformation(string.Format(CultureInfo.InvariantCulture, PreliminaryCheckConstants.PlantDetailsNotAvailabe));
                        }
                    }
                }
                else
                {
                    log.LogInformation(string.Format(CultureInfo.InvariantCulture, PreliminaryCheckConstants.SupportedFileExtensions, queueItem));
                }
            }

            catch (Exception ex)
            {
                log.LogError(string.Format(CultureInfo.InvariantCulture, PreliminaryCheckConstants.FileUploadFailed) + ex.Message);
            }



        }

        private void UpdateIncorrectTemplate(string blobName)
        {
            var processDetails = _processDetailService.GetByFileName(blobName);
            if (processDetails != null)
                UpdatePreCheckStatus(processDetails, preChkStatus: PreliminaryCheckConstants.FailedinPreCheck, summary: PreliminaryCheckConstants.FailedWrongTemplateUsed, processStatus: PreliminaryCheckConstants.FileUploadFailed);
        }

        private void UpdatePreCheckStatus(ProcessDetail processDetails, string preChkStatus = null, string processStatus = null, string summary = null)
        {

            if (preChkStatus != null)
                processDetails.PreChkStatus = preChkStatus;
            if (processStatus != null)
                processDetails.ProcessStatus = processStatus;
            if (summary != null)
                processDetails.Summary = summary;

            _processDetailService.Update(processDetails);
        }
    }
}
