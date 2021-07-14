using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Moq;
using System;
using System.Diagnostics;
using Xunit;
namespace JM.Methanol.UnitTests.Services
{
    
    public class MethanolStorageQueueTests
    {
        private readonly MethanolStorageQueue _storageQueue;

        public MethanolStorageQueueTests()
        {
            _storageQueue = new MethanolStorageQueue();

        }

        [Fact]
        public void Should_Return_Message_From_Queue()
        {
            // Arrange.
            var queueMessage = "test.xlsm";
            var precheckqueue = "pre-chk-queue";

            var uri = new Uri("http://127.0.0.1:10001/devstoreaccount1/pre-chk-queue");

            var storageCredentials = new StorageCredentials();

            var connectionString = "UseDevelopmentStorage=true";

            var queueStorageMock = new Mock<IStorageQueue>();

            var storageMock = new Mock<CloudStorageAccount>();

            var mockCloudQueueClient = new Mock<CloudQueueClient>(uri, storageCredentials);
            storageMock.Setup(x => x.CreateCloudQueueClient()).Returns(mockCloudQueueClient.Object);
            var mockCloudQueue = new Mock<CloudQueue>(uri);
            mockCloudQueueClient.Setup(x => x.GetQueueReference(precheckqueue)).Returns(mockCloudQueue.Object);
            mockCloudQueue.Setup(x => x.CreateIfNotExistsAsync()).ReturnsAsync(true);
            queueStorageMock.Setup(x => x.AddMessageToStorageQueue(connectionString, precheckqueue, queueMessage)).Returns(true);

            //Act
            var isMessageAddedActual=_storageQueue.AddMessageToStorageQueue(connectionString,precheckqueue, queueMessage);
            // Assert.

            Assert.True(isMessageAddedActual);
        }
    }
}
