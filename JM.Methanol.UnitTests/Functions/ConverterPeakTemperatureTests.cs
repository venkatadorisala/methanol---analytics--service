using JM.Integration.Common;
using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using JM.Integration.Methanol.Services.UnitTests.TestHelper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace JM.Methanol.UnitTests.Functions
{
    public class ConverterPeakTemperatureTests
    {

        private const string validSectionId = "1";
        private readonly ConverterPeakTemp _converterPeakTemp;
        private readonly List<IRequestProcessor> _converterPeakTempRequestProcessor = new List<IRequestProcessor>();
        private readonly TelemetryClient _mockTelemetryClient;
        private readonly Mock<IActivityTagger> _activityTagger = new Mock<IActivityTagger>();
        private readonly Mock<IReqValidator> _validator = new Mock<IReqValidator>();
        private readonly Mock<IRequestProcessor> _converterPeakTempRequest = new Mock<IRequestProcessor>();
        private readonly Mock<IBaseDataAccess> _baseDataAccess = new Mock<IBaseDataAccess>();

        public ConverterPeakTemperatureTests()
        {
            var _config = TelemetryConfiguration.CreateDefault();
            _mockTelemetryClient = new TelemetryClient(_config);
            _converterPeakTempRequest.SetupProperty(x => x.TransactionName, "ConverterPeakTemp");
            _converterPeakTempRequestProcessor.Add(_converterPeakTempRequest.Object);
            _converterPeakTemp = new ConverterPeakTemp(_mockTelemetryClient, _activityTagger.Object, _validator.Object, _converterPeakTempRequestProcessor);
        }


        [Fact]
        [Trait("API", "Converter Peak Temp")]
        public async Task Http_trigger_valid_sectionid_returns200()
        {
            //Arrange
            var converterPeakTempResponse = ConverterPeakTempTestHelper.BuildConverterPeakTempObject();
            var context = new DefaultHttpContext();
            _converterPeakTempRequest.Setup(x => x.GetKPIResponse(It.IsAny<string>())).Returns(Task.FromResult(converterPeakTempResponse));
            _validator.Setup(x => x.IsValidRequest(It.IsAny<string>())).Returns(true);
            //Act
            var response = await _converterPeakTemp.Run(context.Request, validSectionId);
            //Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var jsonResponse = JsonConvert.SerializeObject(result.Value);
            var jsonConverterPeakResponse = JsonConvert.SerializeObject(converterPeakTempResponse);
            Assert.IsType<BaseKpiResponse>(result.Value);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }


        [Theory]
        [InlineData("0123")]
        [InlineData("   ")]
        [InlineData("null")]
        [Trait("API", "Converter Peak Temp")]
        public async Task Http_trigger_invalid_sectionId_returns400(string invalidSectionId)
        {
            //Arrange
            BaseKpiResponse converterPeakTempResponse = null;
            var context = new DefaultHttpContext();
            _converterPeakTempRequest.Setup(x => x.GetKPIResponse(It.IsAny<string>())).ReturnsAsync(converterPeakTempResponse);
            _validator.Setup(x => x.IsValidRequest(It.IsAny<string>())).Returns(false);
            //Act
            var response = await _converterPeakTemp.Run(context.Request, invalidSectionId);
            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Contains("Id not found", Convert.ToString(result.Value));
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        [Trait("API", "Converter Peak Temp")]
        public async Task Http_trigger_throws_exception_returns500()
        {
            //Arrange
            var context = new DefaultHttpContext();
            _converterPeakTempRequest.Setup(x => x.GetKPIResponse(It.IsAny<string>())).Throws<Exception>();
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), System.Data.CommandType.StoredProcedure)).Throws<Exception>();
            _validator.Setup(x => x.IsValidRequest(It.IsAny<string>())).Returns(true);
            //Act
            var response = await _converterPeakTemp.Run(context.Request, validSectionId);
            //Assert
            Assert.IsType<ExceptionResult>(response);
        }
    }
}
