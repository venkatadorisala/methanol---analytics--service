using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.ApplicationInsights;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Common;
using System.Collections.Generic;
using System.Linq;
using JM.Integration.Methanol.Services.Models;
using System.Web.Http;

namespace JM.Integration.Methanol.Services.V1
{
    /// <summary>
    ///  Methanol Production Per Converter KPI
    /// </summary>
    public class MethanolProductionPerConverter
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IActivityTagger _activityTagger;
        private readonly string _correlationIdentifier;
        private readonly IReqValidator _validator;
        private readonly IRequestProcessor _methanolProductionPerConverterProcessor;

        /// <summary>
        /// Initializes a new instance of the<see cref="ConverterPeakTemp"/> class.
        /// </summary>
        /// <param name="telemetryClient"></param>
        /// <param name="activityTagger"></param>
        /// <param name="validator"></param>
        /// <param name="methanolProductionPerConverterProcessor"></param>
        public MethanolProductionPerConverter(TelemetryClient telemetryClient, IActivityTagger activityTagger, IReqValidator validator, IEnumerable<IRequestProcessor> methanolProductionPerConverterProcessor)
        {
            this._telemetryClient = telemetryClient;
            this._activityTagger = activityTagger;
            this._correlationIdentifier = Guid.NewGuid().ToString();
            this._validator = validator;
            this._methanolProductionPerConverterProcessor = methanolProductionPerConverterProcessor.FirstOrDefault(x => x.TransactionName == "MethanolProductionPerConverter");
        }

        /// <summary>
        /// Returns response for Methanol Production Per Converter KPI
        /// </summary>
        /// <param name="req"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        
        [FunctionName("MethanolProductionPerConverter")]
      
            public async Task<IActionResult>Run(
                [HttpTrigger(AuthorizationLevel.Function,"get",Route= "section/{sectionId}/kpi/methanolProductionPerConverter")] HttpRequest req,
                string sectionId="")
        {
            var logCollectionSet = new Dictionary<string, string>();
            BaseKpiResponse response;
            try
            {
                _activityTagger.AddTag("ParentId", "MethanolProductionPerConverter");
                if (!_validator.IsValidRequest(sectionId))
                {
                    logCollectionSet.Add("CustomError", "Id not found");
                    logCollectionSet.Add("Sucess", "False");
                    return new BadRequestObjectResult("Id not found");
                }
                else
                {
                    logCollectionSet.Add("Success", "True");
                    response = await _methanolProductionPerConverterProcessor.GetKPIResponse(sectionId).ConfigureAwait(true);
                }
                if (response == null)
                {
                    logCollectionSet.Add("SectionIdNotFound", "True");
                    _activityTagger.AddTag("SectionIdNotFound", "True");
                    return new BadRequestObjectResult("Id not found");
                }
                _activityTagger.AddTag("Error", "false");
                logCollectionSet.Add("Error", "false");
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _activityTagger.AddTag("Error", ex.Message);
                _telemetryClient.TrackException(ex);
                logCollectionSet.Add("Error Message", ex.Message);
                return new ExceptionResult(ex, false);
            }
            finally
            {
                _telemetryClient.TrackTrace(this._correlationIdentifier, logCollectionSet);
            }
          }
        }
    }

