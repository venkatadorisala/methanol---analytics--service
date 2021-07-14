using JM.Integration.Common;
using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.V1;
using JM.Methanol.UnitTests.TestHelper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace JM.Methanol.UnitTests.Functions
{
    public class MethanolProductionPerConverterTests
    {
        private const string validSectionId = "1";
        private readonly MethanolProductionPerConverter _methanolProductionPerConverter;

        private readonly List<IRequestProcessor> _methanolProductionPerConverterProcessor = new List<IRequestProcessor>();
        private readonly TelemetryClient _mockTelemetryClient;
        private readonly Mock<IActivityTagger> _activityTagger = new Mock<IActivityTagger>();
        private readonly Mock<IReqValidator> _validator = new Mock<IReqValidator>();
        private readonly Mock<IRequestProcessor> _methanolProductionProcessor = new Mock<IRequestProcessor>();
        private readonly Mock<IBaseDataAccess> _baseDataAccess = new Mock<IBaseDataAccess>();

        public MethanolProductionPerConverterTests()
        {
            var _config = TelemetryConfiguration.CreateDefault();
            _mockTelemetryClient = new TelemetryClient(_config);
            _methanolProductionProcessor.SetupProperty(x => x.TransactionName, "MethanolProductionPerConverter");
            _methanolProductionPerConverterProcessor.Add(_methanolProductionProcessor.Object);
            _methanolProductionPerConverter = new MethanolProductionPerConverter(_mockTelemetryClient, _activityTagger.Object, _validator.Object, _methanolProductionPerConverterProcessor);
        
        }

        [Fact]
        [Trait("API", "MethanolProductionPerConverter")]
        public async Task Http_trigger_valid_sectionid_returns200()
        {
            //Arrange
            var methanolProductionResponse = MethanolProductionPerConverterHelper.BuildConverterPeakTempObject();
            var context = new DefaultHttpContext();
            _methanolProductionProcessor.Setup(x => x.GetKPIResponse(It.IsAny<string>())).Returns(Task.FromResult(methanolProductionResponse));
            _validator.Setup(x => x.IsValidRequest(It.IsAny<string>())).Returns(true);
            //Act
            var response = await _methanolProductionPerConverter.Run(context.Request, validSectionId);
            //Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var jsonResponse = JsonConvert.SerializeObject(result.Value);
            var jsonMethanolProductionResponse = JsonConvert.SerializeObject(methanolProductionResponse);
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
            BaseKpiResponse methanolProductionResponse = null;
            var context = new DefaultHttpContext();
            _methanolProductionProcessor.Setup(x => x.GetKPIResponse(It.IsAny<string>())).ReturnsAsync(methanolProductionResponse);
            _validator.Setup(x => x.IsValidRequest(It.IsAny<string>())).Returns(false);
            //Act
            var response = await _methanolProductionPerConverter.Run(context.Request, invalidSectionId);
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
            _methanolProductionProcessor.Setup(x => x.GetKPIResponse(It.IsAny<string>())).Throws<Exception>();
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), System.Data.CommandType.StoredProcedure)).Throws<Exception>();
            _validator.Setup(x => x.IsValidRequest(It.IsAny<string>())).Returns(true);
            //Act
            var response = await _methanolProductionPerConverter.Run(context.Request, validSectionId);
            //Assert
            Assert.IsType<ExceptionResult>(response);
        }

    }
}
