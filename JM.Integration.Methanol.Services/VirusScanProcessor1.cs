// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Methanol.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using nClam;
    using System.Linq;


    /// <summary>
    /// ExamScheduleSetupAndConfigProcessor.
    /// </summary>
    public class VirusScanProcessor1
    {
        private readonly IEnumerable<IMethanolValidator> iScheduleValidator;
        private readonly IMethanolHttpService httpService;
        private readonly string correlationIdentifier;

        /// <summary>
        /// Initializes a new instance of the<see cref="VirusScanProcessor1"/> class.
        /// </summary>
        /// <param name=""></param>
        /// <param name="httpService"></param>
        /// <param name="mapper"></param>
        public VirusScanProcessor1( )
        {


        }

        /// <summary>
        /// ExamScheduleSetupAndConfigProcessor.
        /// </summary>
        /// <param name=""></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ClamAVScanFunction")]
        public  IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = null)] HttpRequest req, ILogger log)
        {
            string name = null;
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var clam = new ClamClient("localhost", 3310);
            var connectionString = System.Environment.GetEnvironmentVariable("serviceBusConnectionString");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Connect to the blob storage
            CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
            // Connect to the blob container
            CloudBlobContainer container = serviceClient.GetContainerReference("test");
            // Connect to the blob file
            CloudBlockBlob blob = container.GetBlockBlobReference($"virtual-health-solution.vsdx");
            // Get the blob file as text
           string contents = blob.DownloadTextAsync().Result;


            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStreamAsync(memoryStream).Result;
                var length = memoryStream.Length;
             var   text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
              var scanResult1 = clam.SendAndScanFileAsync(contents).Result;
            }


            // Create client
          

            // Scanning for viruses...
            var scanResult = clam.SendAndScanFileAsync(contents).Result;

            switch (scanResult.Result)
            {
                case ClamScanResults.Clean:
                    responseMessage = "The file is clean!";
                    log.LogInformation("The file is clean!");
                    break;
                case ClamScanResults.VirusDetected:
                    log.LogInformation("Virus Found!");
                    log.LogInformation("Virus name: {0}", scanResult.InfectedFiles.First().VirusName);
                    break;
                case ClamScanResults.Error:
                    responseMessage = scanResult.RawResult;
                    log.LogInformation("Error scanning file: {0}", scanResult.RawResult);
                    break;
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
