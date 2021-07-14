using JM.Integration.Methanol.Services.Constants;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace JM.Integration.Methanol.Services
{
    /// <summary>
    /// Syngas Split API
    /// </summary>
    public class SyngasSplitV1
    {
        private readonly ISyngasSplitService _syngassplitService;
        private readonly TelemetryClient _telemetryClient;
        private readonly IActivityTagger _activityTagger;
        private readonly string _correlationIdentifier;

        /// <summary>
        /// Initializes new instance of the<see cref="SyngasSplitV1" />class
        /// </summary>
        /// <param name="syngasSplitService"></param>
        /// <param name="telemetryClient"></param>
        /// <param name="activityTagger"></param>
        public SyngasSplitV1(ISyngasSplitService syngasSplitService, TelemetryClient telemetryClient, IActivityTagger activityTagger)
        {
            _syngassplitService = syngasSplitService;
            _telemetryClient = telemetryClient;
            _activityTagger = activityTagger;
            _correlationIdentifier = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns the response for Syngas Split KPI
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="sectionId"></param>
        /// <returns>Returns response as <see cref="ObjectResult"/> type depending on function output</returns>
        [FunctionName("SyngasSplitV1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "section/{sectionId}/kpi/syngasSplit")] HttpRequest req,
            ILogger log, string sectionId = "")
        {
            var logCollection = new Dictionary<string, string>();
            SyngasSplit response = null;

            try
            {
                _activityTagger.AddTag("ParentId", "SyngasSplit");
                if (string.IsNullOrWhiteSpace(sectionId))
                {
                    logCollection.Add("CustomError", MethanolConstants.InvalidSectionId);
                    logCollection.Add("Success", "false");
                    return new BadRequestObjectResult(MethanolConstants.InvalidSectionId);
                }

                logCollection.Add("Success", "True");
                response = await _syngassplitService.GetSyngasSplitResponse(sectionId).ConfigureAwait(true);

                if (response == null)
                {
                    logCollection.Add("SectionIdNotFound", "True");
                    _activityTagger.AddTag("SectionIdNotFound", "True");
                    return new BadRequestObjectResult(MethanolConstants.InvalidSectionId);
                }
                _activityTagger.AddTag("Error", "false");
                logCollection.Add("Error", "false");
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _activityTagger.AddTag("Error", ex.Message);
                _telemetryClient.TrackException(ex);
                logCollection.Add("Error Message", ex.Message);
                return new ExceptionResult(ex, false);
            }
            finally
            {
                _telemetryClient.TrackTrace(_correlationIdentifier, logCollection);
            }
        }
    }
}