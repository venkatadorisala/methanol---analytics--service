using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.Extensions;
using JM.Integration.Methanol.Services;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using JM.Integration.Methanol.Services.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using nClam;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ClamScanResult = JM.Integration.Methanol.Common.Enums.ClamScanResult;

namespace JM.Methanol.UnitTests.Services
{
    public class MethanolScanFileTests
    {
        private readonly MethanolScanFile _ScanFileAsync;
        private readonly Mock<IScanFileAsync> _methanolScanFile;
        private Mock<IClamClient> _clamavClient;
        private readonly Mock<IBlobService> _blobService;
        private readonly Mock<IStorageQueue> _storageQueue;
        private readonly Mock<IProcessDetailService> _processDetailService;
        private readonly ILogger<ScanUploadedFileV1> _logger;

        

        public MethanolScanFileTests()
        {
            _methanolScanFile = new Mock<IScanFileAsync>();
            _clamavClient = new Mock<IClamClient>();
            _blobService = new Mock<IBlobService>();
            _storageQueue = new Mock<IStorageQueue>();
            _processDetailService = new Mock<IProcessDetailService>();
            _logger = new NullLogger<ScanUploadedFileV1>();

            _ScanFileAsync =new MethanolScanFile(_clamavClient.Object, _blobService.Object, _storageQueue.Object,_processDetailService.Object);
        }

        [Fact]
        public void ClamAV_FileScan_Should_NotRun_Successfully_For_WrongPort()
        {
            //Arrange
            string fileContent = "upload sample data";
            var fileStream = new MemoryStream(Encoding.Default.GetBytes(fileContent));
             _clamavClient.Object.Port = 310;
            _clamavClient.Object.Server = "localhost";
            var results = _methanolScanFile.Setup(x => x.ScanAntvirusCheckAsync(fileStream)).Returns(GetResult);

            //Act
           var scanResult= _ScanFileAsync.ScanAntvirusCheckAsync(fileStream);

            //Assert
            _methanolScanFile.Verify(x => x.ScanAntvirusCheckAsync(fileStream), Times.Never);
        }

        [Fact]
        public void Should_Process_Response_When_Getting_CleanScanResult()
        {
            //Arrange
            var scanResult = new nClam.ClamScanResult(@"C:\test.txt: OK");
            string blobName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm";
            var expectedResult = true;
            var updatedProcessDetail = new ProcessDetail { Sid = 1, PlantPlantSid = "1", UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = null };
            var results = _methanolScanFile.Setup(x => x.ProcessClamScanResult(scanResult,updatedProcessDetail, blobName,_logger)).Returns(expectedResult);
            var queueMessage = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm";
            var queueMessageExpectedResult = true;
            var connectionString = "UseDevelopmentStorage=true";
            var precheckqueue = "pre-chk-queue";
            _storageQueue.Setup(x => x.AddMessageToStorageQueue(connectionString, precheckqueue,queueMessage)).Returns(queueMessageExpectedResult);
            //Act
            var actualResult = _ScanFileAsync.ProcessClamScanResult(scanResult,updatedProcessDetail, blobName, _logger);

            //Assert
            Assert.Equal(expectedResult,actualResult);
        }

        [Theory]
        [InlineData(@"C:\test.txt: Error")]
        [InlineData("unknown")]
        public void Should_Process_Response_When_Getting_ErrorScanResult(string clamResponse)
        {
            //Arrange
            var scanResult = new nClam.ClamScanResult(clamResponse);
            string blobName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm";
            var expectedResult = true;
            var updatedProcessDetail = new ProcessDetail { Sid = 1, PlantPlantSid = "1", UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = null };
            var results = _methanolScanFile.Setup(x => x.ProcessClamScanResult(scanResult,updatedProcessDetail, blobName, _logger)).Returns(expectedResult);
            var blobDeleteExpectedResult = true;
            var blobContainer = "raw-data-storage";
            var connectionString = "UseDevelopmentStorage=true"; 
            _blobService.Setup(x => x.DeleteBlob(blobName)).Returns(blobDeleteExpectedResult);
            //Act
            var actualResult = _ScanFileAsync.ProcessClamScanResult(scanResult,updatedProcessDetail, blobName, _logger);

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void Should_Process_Response_When_Getting_VirusDectedScanResult()
        {
            //Arrange
            var scanResult = new nClam.ClamScanResult(@"\\?\C:\test.txt: Eicar-Test-Signature FOUND");
            string blobName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm";
            var expectedResult = true;
            var updatedProcessDetail = new ProcessDetail { Sid = 1, PlantPlantSid = "1", UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = null };
            var results = _methanolScanFile.Setup(x => x.ProcessClamScanResult(scanResult, updatedProcessDetail, blobName, _logger)).Returns(expectedResult);
            var blobDeleteExpectedResult = true;
            var blobContainer = "raw-data-storage";
            var connectionString = "UseDevelopmentStorage=true";
            _blobService.Setup(x => x.DeleteBlob(blobName)).Returns(blobDeleteExpectedResult);
            //Act
            var actualResult = _ScanFileAsync.ProcessClamScanResult(scanResult, updatedProcessDetail,blobName, _logger);

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void Should_Update_ScanStatus_When_FileIsCleaned()
        {
            //Arrange

            var scanResult = new nClam.ClamScanResult(@"\\?\C:\test.txt: Eicar-Test-Signature FOUND");
            string blobName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm";
            var expectedResult = true;
            var updatedProcessDetail = new ProcessDetail { Sid = 1, PlantPlantSid = "1", UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = null };
            var results = _methanolScanFile.Setup(x => x.UpdateScanStatus(ClamScanResult.Clean,updatedProcessDetail, blobName, _logger)).Returns(expectedResult);
            _processDetailService.Setup(x => x.GetByFileName(blobName)).Returns(updatedProcessDetail);
            updatedProcessDetail.AvScanStatus= EnumExtensionMethods.GetEnumDescription(ClamScanResult.Clean);
            var isStatusUpdated = true;
            _processDetailService.Setup(x => x.Update(updatedProcessDetail)).Returns(isStatusUpdated);

            //Act
            var actualResult = _ScanFileAsync.UpdateScanStatus(ClamScanResult.Clean, updatedProcessDetail,blobName, _logger);

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void Should_Update_ScanStatus_When_FileIsInfected()
        {
            //Arrange

            var scanResult = new nClam.ClamScanResult(@"\\?\C:\test.txt: Eicar-Test-Signature FOUND");
            string blobName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm";
            var expectedResult = true;
            var updatedProcessDetail = new ProcessDetail { Sid = 1, PlantPlantSid = "1", UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = null };
            var results = _methanolScanFile.Setup(x => x.UpdateScanStatus(ClamScanResult.Clean,updatedProcessDetail, blobName, _logger)).Returns(expectedResult);
            _processDetailService.Setup(x => x.GetByFileName(blobName)).Returns(updatedProcessDetail);
            updatedProcessDetail.AvScanStatus = EnumExtensionMethods.GetEnumDescription(ClamScanResult.Error);
            var isStatusUpdated = true;
            _processDetailService.Setup(x => x.Update(updatedProcessDetail)).Returns(isStatusUpdated);

            //Act
            var actualResult = _ScanFileAsync.UpdateScanStatus(ClamScanResult.Clean,updatedProcessDetail, blobName, _logger);

            //Assert
            Assert.True(actualResult);
        }

        private Task<nClam.ClamScanResult> GetResult()
        {
            Task<nClam.ClamScanResult> result = new Task<nClam.ClamScanResult>(GetClamScanResult);
            return result;
        }

        private nClam.ClamScanResult GetClamScanResult()
        {
            return new nClam.ClamScanResult(@"C:\test.txt: OK");
        }

    }
}
