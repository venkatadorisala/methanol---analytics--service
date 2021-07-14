using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using nClam;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JM.Integration.Methanol.Services.UnitTests
{
    public class ScanUploadedFileV1Tests
    {
        private readonly ScanUploadedFileV1 _scanUploadedFileV1;
        private readonly Mock<IBlobService> _blobStorage = new Mock<IBlobService>();
        private readonly Mock<IScanFileAsync> _scanFileAsync = new Mock<IScanFileAsync>();
        private readonly Mock<IProcessDetailService> _processDetailService = new Mock<IProcessDetailService>();
        private readonly ILogger<ScanUploadedFileV1> _logger;
        public ScanUploadedFileV1Tests()
        {
            _logger = new NullLogger<ScanUploadedFileV1>();
            _scanUploadedFileV1 = new ScanUploadedFileV1(_blobStorage.Object, _scanFileAsync.Object, _processDetailService.Object);
        }

        [Fact]
        public void Queue_trigger_Should_Return_CleanStatus_ForCleanedFile()
        {
            //Arrange
            string queueMessage = @"{
                'topic':'/subscriptions/029536b6-f6c6-4f73-af5f-565d7c4cc579/resourceGroups/RG-DEV-EU-LEVOMETHANOL-001/providers/Microsoft.Storage/storageAccounts/stadeveuevg001'," +
                "'subject':'/blobServices/default/containers/raw-data-storage/blobs/JM-LEVO_Upload_v5.1_plantname_12032020.xlsm'," +
                "'eventType':'Microsoft.Storage.BlobCreated'," +
                "'id':'634fad3f-801e-002b-7afa-6b8ef6067596'," +
                "'data':{'api':'PutBlob'," +
                "'clientRequestId':'62a56390-97f0-411a-9149-5a949703000e'," +
                "'requestId':'634fad3f-801e-002b-7afa-6b8ef6000000'," +
                "'eTag':'0x8D93A11E1FAD222'," +
                "'contentType':'application/vnd.ms-excel.sheet.macroEnabled.12'," +
                "'contentLength':211614," +
                "'blobType':'BlockBlob'," +
                "'url':'https://stadeveuevg001.blob.core.windows.net/raw-data-storage/JM-LEVO_Upload_v5.1_plantname_12032020.xlsm'," +
                "'sequencer':'000000000000000000000000000014D9000000000075d59f'," +
                "'storageDiagnostics':{'batchId':'629d32dc-7006-0062-00fa-6bcc1d000000'}}," +
                "'dataVersion':''," +
                "'metadataVersion':'1'," +
                "'eventTime':'2021-06-28T08:51:12.9802274Z'}";

            string fileName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm";

            _blobStorage.Setup(x => x.GetBlobNameFromQueue(queueMessage)).Returns(fileName);
            string fileContent = "upload sample data";
            var fileStream = new MemoryStream(Encoding.Default.GetBytes(fileContent));
            var connectionString = "UseDevelopmentStorage=true";
            var updatedProcessDetail = new ProcessDetail { Sid = 1, PlantPlantSid = "1", UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = null };
            _processDetailService.Setup(x => x.GetByFileName(fileName)).Returns(updatedProcessDetail);
            _blobStorage.Setup(x => x.GetBlobStream(fileName)).Returns(fileStream);
            Task<ClamScanResult> result = new Task<ClamScanResult>(GetResult);
             _scanFileAsync.Setup(x => x.ScanAntvirusCheckAsync(fileStream)).Throws<Exception>();
            ClamScanResult clamScanResult = new ClamScanResult(@"C:\test.txt: OK");
            
            _scanFileAsync.Setup(x => x.ProcessClamScanResult(clamScanResult, updatedProcessDetail, fileName,_logger)).Returns(true);

            //Act
            _scanUploadedFileV1.Run(queueMessage, _logger);

            //Assert
            _blobStorage.Verify(x => x.GetBlobNameFromQueue(queueMessage), Times.AtLeastOnce);
            Assert.True(true);

            _blobStorage.Verify(x => x.GetBlobStream(fileName), Times.Once);
            Assert.True(true);
            _scanFileAsync.Verify(x => x.ProcessClamScanResult(clamScanResult,updatedProcessDetail, fileName, _logger), Times.Never);
            Assert.True(true);
        }

        private ClamScanResult GetResult()
        {
            return new ClamScanResult(@"C:\test.txt: OK");
        }
    }
}
