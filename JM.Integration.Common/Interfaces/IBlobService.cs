using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JM.Integration.Common.Interfaces
{
    /// <summary>
    /// Contains methods for blob storage services implementation
    /// </summary>
    public interface IBlobService
    {
        /// <summary>
        /// Upload file into blob storage
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileStream"></param>
        /// <param name="containerName"></param>
        /// <returns>Retuns boolean for uploaded status</returns>
        Task<bool> UploadBlob(string name, System.IO.Stream fileStream, string containerName);

        /// <summary>
        /// Getting file from blob storage
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns>Returns file content as stream</returns>
        Stream GetBlobStream(string blobName);

        /// <summary>
        /// Deleting file from blob container
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns></returns>
        bool DeleteBlob(string blobName);

        /// <summary>
        /// Getting file name by parsing queue message
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <returns>Returns file name</returns>
        string GetBlobNameFromQueue(string queueMessage);

    }
}
