using Azure.Storage.Blobs;
using JM.Integration.Methanol.Services.Services;
using Microsoft.Extensions.Azure;
using Moq;
using Xunit;
namespace JM.Methanol.UnitTests.Services
{

    public class MethanolBlobServiceTests
    {
        MethanolBlobService _methanolBlobService;
        private readonly Mock<BlobServiceClient> _blobServiceMockGen1 = new Mock<BlobServiceClient>();
        private readonly Mock<IAzureClientFactory<BlobServiceClient>> _blobServiceMockGen2 = new Mock<IAzureClientFactory<BlobServiceClient>>();
        public MethanolBlobServiceTests()
        {
            var blobServiceGen1Client = new BlobServiceClient("UseDevelopmentStorage=true");
            IAzureClientFactory<BlobServiceClient> clientFactory = null;
            _methanolBlobService = new MethanolBlobService(blobServiceGen1Client, clientFactory);
            _methanolBlobService = new MethanolBlobService(_blobServiceMockGen1.Object, _blobServiceMockGen2.Object);

        }

        [Fact]
        public void Should_Return_FileName_when_Queue_HasMessage()
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

            //Arrange
            var blobclient = new Mock<BlobClient>();
            var blobServiceClientMock = new Mock<BlobServiceClient>();
            var blobContainerClientMock = new Mock<BlobContainerClient>("UseDevelopmentStorage=true", "AvCheckBlobContainer");
            blobContainerClientMock.Setup(x => x.GetBlobClient("blob")).Returns(blobclient.Object);
            blobServiceClientMock.Setup(x => x.GetBlobContainerClient("AvCheckBlobContainer")).Returns(blobContainerClientMock.Object);

            //Act

            var fileContent = _methanolBlobService.GetBlobNameFromQueue(queueMessage);

            //Assert

            Assert.Equal("JM-LEVO_Upload_v5.1_plantname_12032020.xlsm", fileContent);

        }
    }
}
