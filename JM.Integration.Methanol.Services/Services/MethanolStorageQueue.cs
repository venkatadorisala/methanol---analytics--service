using JM.Integration.Common.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;

namespace JM.Integration.Methanol.Services.Services
{
    /// <inheritdoc cref="IStorageQueue"/>
    public class MethanolStorageQueue : IStorageQueue
    {
        public bool AddMessageToStorageQueue(string connectionString, string queueName, string message)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExistsAsync();

            CloudQueueMessage queueMessage = new CloudQueueMessage(message);

            queue.AddMessageAsync(queueMessage);

            return true;
        }
    }
}