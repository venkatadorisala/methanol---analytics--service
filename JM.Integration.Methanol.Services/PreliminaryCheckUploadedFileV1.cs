using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using System.Linq;
using System;


namespace JM.Integration.Methanol.Services
{
    public class PreliminaryCheckUploadedFileV1
    {
       
        private readonly IBlobStorage _blobStorage;
        private readonly IFileExtensionValidationService _fileExtensionValidationService;
        private readonly IExcelFileContentExtractorService _excelFileContentExtractorService;
        private readonly IPlantService _plantService;
        private readonly IProcessDetailService _processDetailService;
        private readonly IBlobService _blobService;

        public PreliminaryCheckUploadedFileV1(IBlobStorage blobStorage, IFileExtensionValidationService fileExtensionValidationService,
            IPlantService plantService, IExcelFileContentExtractorService excelFileContentExtractorService, IProcessDetailService processDetailService, IBlobService blobService)
        {
            _fileExtensionValidationService = fileExtensionValidationService;
            _plantService = plantService;
            _excelFileContentExtractorService = excelFileContentExtractorService;
            _processDetailService = processDetailService;
            _blobService = blobService;
        }

        [FunctionName("PreliminaryCheckUploadedFileV1")]
        public  async  Task Run([QueueTrigger("pre-chk-queue", Connection = "AzureWebJobsStorage")] string queueItem, ILogger log)
        {

            log.LogInformation($"Started premliminary check of  {queueItem}  ", queueItem);

            try
            {
                var isValidFileExtension = _fileExtensionValidationService.IsValidExtension(queueItem);
                if (isValidFileExtension)
                {
                    log.LogInformation($"Reading file from blob");
                    
                    //Get blob name from AV check queue.
                    string blobName = queueItem;   //_blobStorage.GetBlobNameFromQueue(queueItem);
                    
                    //Fetch file stream from blob.
                    var filecontent = _blobService.GetBlobStream(blobName);
                                       
                    //Read excel template content.
                    var excelFileContent = _excelFileContentExtractorService.GetFileContent(filecontent);

                    if (excelFileContent != null)
                    {
                        //Fetch Process Details by Plant_Sid.
                        var processDetails = _processDetailService.GetProcessDetailsByPlantSid(excelFileContent.PlantSid);
                        if (processDetails != null)
                        {
                            var plant = processDetails.PlantPlantS;
                            if (plant != null)
                            {
                                //If data template version exisists 
                                if (plant.MasterTemplates.Where(x => x.PlantPlantSid == plant.PlantSid).FirstOrDefault().DataTemplates.Any(x => x.TemplateVersion == excelFileContent.TemplateVersion))
                                {
                                    UpdatePreCheckStatus(processDetails, "Success");
                                    await _blobService.UploadBlob(queueItem, /*_blobService.GetBlobStream(blobName)*/ filecontent, Environment.GetEnvironmentVariable("afterPrechkBlobContainer")).ConfigureAwait(true); ;
                                    filecontent.Close();
                                }
                                else
                                {
                                    UpdatePreCheckStatus(processDetails, "Failed due to incorrect version.");
                                }
                            }
                            else
                            {
                                //Incorrect plant Id or name.
                                UpdatePreCheckStatus(processDetails, "Failed as wrong template is used.");
                            }
                        }
                        else
                        {
                            processDetails.PreChkStatus = "File upload failed.";
                            _processDetailService.Update(processDetails);
                        }
                    }
                }
                else
                {
                    log.LogInformation($"extension of uploaded file {queueItem} is not supported. supported extensions are: {SupportedFileTypes.XLSM } and {SupportedFileTypes.XLSX}");
                }
            }
            catch (Exception ex)
            {
                log.LogError("Issue in file scaning  : " + ex.Message);
            }
           
        }

        private void UpdatePreCheckStatus(ProcessDetail processDetails, string preChkStatus)
        {
            processDetails.PreChkStatus = preChkStatus;
            _processDetailService.Update(processDetails);
        }
    }
}
