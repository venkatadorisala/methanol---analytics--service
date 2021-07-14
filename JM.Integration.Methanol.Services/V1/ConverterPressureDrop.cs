// Copyright (c) International Baccalaureate Organization 2021. All rights reserved.

using JM.Integration.Common;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace JM.Integration.Methanol.Services
{
    /// <summary>
    ///  Converter Peak Temp KPI
    /// </summary>
    public class ConverterPressureDropKPI
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IActivityTagger _activityTagger;
        private readonly string _correlationIdentifier;
        private readonly IReqValidator _validator;
        private readonly IRequestProcessor _converterPressureDropRequestProcessors;

        /// <summary>
        /// Initializes a new instance of the<see cref="ConverterPeakTemp"/> class.
        /// </summary>
        /// <param name="telemetryClient"></param>
        /// <param name="activityTagger"></param>
        /// <param name="validator"></param>
        /// <param name="converterPeakTempRequestProcessor"></param>

        public ConverterPressureDropKPI(TelemetryClient telemetryClient, IActivityTagger activityTagger, IReqValidator validator, IEnumerable<IRequestProcessor> converterPressureDropRequestProcessors)
        {
            this._telemetryClient = telemetryClient;
            this._activityTagger = activityTagger;
            this._correlationIdentifier = Guid.NewGuid().ToString();
            this._validator = validator;
            this._converterPressureDropRequestProcessors = converterPressureDropRequestProcessors.FirstOrDefault(x => x.TransactionName == "ConverterPressureDrop");
        }

        /// <summary>
        /// ExamScheduleSetupAndConfigProcessor.
        /// </summary>
        /// <param name="converterPeakTemp"></param>
        /// <param name="log"></param>
        /// <returns></returns>

        [FunctionName("ConverterPressureDrop")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "section/{sectionId}/kpi/converterPressureDrop")] HttpRequest req,
             string sectionId = "")
        {
            var logCollectionSet = new Dictionary<string, string>();
            BaseKpiResponse response;
            try
            {
                _activityTagger.AddTag("ParentId", "converterPressureDrop");
                if (!_validator.IsValidRequest(sectionId))
                {
                    logCollectionSet.Add("CustomError", "Id ,not found");
                    logCollectionSet.Add("Success", "False");
                    return new BadRequestObjectResult("Id ,not found");
                }
                else
                {
                    logCollectionSet.Add("Success", "True");
                    response = await _converterPressureDropRequestProcessors.GetKPIResponse(sectionId).ConfigureAwait(true);
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