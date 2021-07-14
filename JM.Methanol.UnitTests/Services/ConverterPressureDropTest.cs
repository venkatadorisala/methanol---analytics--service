using JM.Integration.Common;
using JM.Integration.Common.Services;
using JM.Integration.Methanol.Services;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using JM.Methanol.UnitTests.TestHelper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JM.Methanol.UnitTests.Services
{
    public class ConverterPressureDropTest
    {
        private readonly IActivityTagger _mockactivityTagger;
        private readonly string _correlationIdentifier;
        private readonly IReqValidator _mockRequestValidator;
        private readonly Mock<IRequestProcessor> _mockRequestProcessor;
        private readonly TelemetryClient _mockTelemetryClient;
        private readonly IEnumerable<IRequestProcessor> listRequestProcessor;

        private readonly ConverterPressureDropKPI _converterPressureDropKPI;

        public ConverterPressureDropTest()
        {
            this._mockTelemetryClient = this.InitializeMockTelemetryChannel();
            this._mockactivityTagger = new ActivityTagger();
            //this._mockRequestProcessor = new Mock<IEnumerable< IRequestProcessor>>();
            this._mockRequestProcessor = new Mock<IRequestProcessor>();
            var mockProcessorList = new List<IRequestProcessor>();
            this._mockRequestProcessor.SetupProperty(x => x.TransactionName, "ConverterPressureDrop");
            mockProcessorList.Add(_mockRequestProcessor.Object);

            this._mockRequestValidator = new ReqValidator();
            this._converterPressureDropKPI = new ConverterPressureDropKPI(this._mockTelemetryClient, this._mockactivityTagger, this._mockRequestValidator, mockProcessorList);
            // this._mockRequestProcessor.Object.GetEnumerator().Current.TransactionName = "ConverterPressureDrop";
        }

        private TelemetryClient InitializeMockTelemetryChannel()
        {
            // Application Insights TelemetryClient doesn't have an interface (and is sealed)
            // Spin -up our own homebrew mock object
            MockTelemetryChannel mockTelemetryChannel = new MockTelemetryChannel();
            TelemetryConfiguration mockTelemetryConfig = new TelemetryConfiguration
            {
                TelemetryChannel = mockTelemetryChannel,
                InstrumentationKey = Guid.NewGuid().ToString(),
            };

            TelemetryClient mockTelemetryClient = new TelemetryClient(mockTelemetryConfig);
            return mockTelemetryClient;
        }

        [Fact]
        public async Task Run_InvalidRequest_BadRequestObjectResultAsync()
        {
            var reqMock = new Mock<HttpRequest>();
            var response = await this._converterPressureDropKPI.Run(reqMock.Object, "");
            ObjectResult objectResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(objectResponse.StatusCode, 400);
        }

        [Fact]
        public async Task Run_ValidRequestWithNoRecords_Response()
        {
            var reqMock = new Mock<HttpRequest>();
            var baseKPIResp = new BaseKpiResponse();
            this._mockRequestProcessor.Setup(x => x.GetKPIResponse(It.IsAny<string>())).Returns(Task.FromResult(baseKPIResp));

            var response = await this._converterPressureDropKPI.Run(reqMock.Object, "1");
            ObjectResult objectResponse = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(objectResponse.Value.Equals(baseKPIResp), true);
        }
    }
}