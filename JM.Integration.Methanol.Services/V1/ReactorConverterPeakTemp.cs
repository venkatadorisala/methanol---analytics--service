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
    public class ReactorConverterPeakTemp
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IActivityTagger _activityTagger;
        private readonly string _correlationIdentifier;
        private readonly IReqValidator _validator;
        private readonly IRequestProcessor _reactorConverterPeakTempRequestProcessor;

        public ReactorConverterPeakTemp(TelemetryClient telemetryClient, IActivityTagger activityTagger, IReqValidator validator, IEnumerable<IRequestProcessor> reactorConverterPeakTempRequestProcessor)
        {
            this._telemetryClient = telemetryClient;
            this._activityTagger = activityTagger;
            this._correlationIdentifier = Guid.NewGuid().ToString();
            this._validator = validator;
            this._reactorConverterPeakTempRequestProcessor = reactorConverterPeakTempRequestProcessor.FirstOrDefault(x => x.TransactionName == "ReactorConverterPeakTemp");


        }

        [FunctionName("ReactorConverterPeakTemp")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/reactor/{reactorId}/kpi/converterPeakTemp")] HttpRequest req,
             string reactorId = "")
        {
            var logCollectionSet = new Dictionary<string, string>();
            BaseKpiResponse response;
            try
            {
                _activityTagger.AddTag("ParentId", "ConverterPeakTemp");
                if (!_validator.IsValidRequest(reactorId))
                {
                    logCollectionSet.Add("CustomError", "Id not found");
                    logCollectionSet.Add("Success", "False");
                    return new BadRequestObjectResult("Id not found");
                }
                else
                {
                    logCollectionSet.Add("Success", "True");
                    response = await _reactorConverterPeakTempRequestProcessor.GetKPIResponse(reactorId).ConfigureAwait(true);
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
