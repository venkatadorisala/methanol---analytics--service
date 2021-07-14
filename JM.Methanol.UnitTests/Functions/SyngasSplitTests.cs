using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.UnitTests.TestHelper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace JM.Integration.Methanol.Services.UnitTests
{
    public class SyngasSplitTests
    {
        private const string validSectionId = "1";
        private readonly Mock<ISyngasSplitService> _syngasSplitService = new Mock<ISyngasSplitService>();
        private readonly TelemetryConfiguration config = TelemetryConfiguration.CreateDefault();
        private readonly TelemetryClient _mockTelemetryClient;
        private readonly SyngasSplitV1 _syngasSplitV1;
        private readonly ILogger<SyngasSplitV1> _logger;
        private readonly Mock<IActivityTagger> _activityTagger = new Mock<IActivityTagger>();

        public SyngasSplitTests()
        {
            _logger = new NullLogger<SyngasSplitV1>();
            _mockTelemetryClient = new TelemetryClient(config);
            _syngasSplitV1 = new SyngasSplitV1(_syngasSplitService.Object, _mockTelemetryClient, _activityTagger.Object);
        }

        [Fact]
        [Trait("API", "Syngas Split")]
        public async Task Http_trigger_valid_sectionid_returns200()
        {
            //Arrange
            var syngasSplitResponse = SyngasSplitTestHelper.BuildSyngasSplitObject();
            var context = new DefaultHttpContext();
            _syngasSplitService.Setup(x => x.GetSyngasSplitResponse(It.IsAny<string>())).ReturnsAsync(syngasSplitResponse);
            //Act
            var response = await _syngasSplitV1.Run(context.Request, _logger, validSectionId);
            //Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var jsonResponse = JsonConvert.SerializeObject(result.Value);
            var jsonSyngasResponse = JsonConvert.SerializeObject(syngasSplitResponse);
            Assert.IsType<SyngasSplit>(result.Value);
            Assert.Equal(jsonSyngasResponse, jsonResponse);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Theory]
        [InlineData("0123")]
        [InlineData("   ")]
        [InlineData("null")]
        [Trait("API", "Syngas Split")]
        public async Task Http_trigger_invalid_sectionId_returns400(string invalidSectionId)
        {
            //Arrange
            SyngasSplit syngasSplitResponse = null;
            var context = new DefaultHttpContext();
            _syngasSplitService.Setup(x => x.GetSyngasSplitResponse(It.IsAny<string>())).ReturnsAsync(syngasSplitResponse);
            //Act
            var response = await _syngasSplitV1.Run(context.Request, _logger, invalidSectionId);
            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Contains("Id not found", Convert.ToString(result.Value));
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        [Trait("API", "Syngas Split")]
        public async Task Http_trigger_throws_exception_returns500()
        {
            //Arrange
            var context = new DefaultHttpContext();
            _syngasSplitService.Setup(x => x.GetSyngasSplitResponse(It.IsAny<string>())).Throws<Exception>();
            //Act
            var response = await _syngasSplitV1.Run(context.Request, _logger, validSectionId);
            //Assert
            Assert.IsType<ExceptionResult>(response);
        }
    }
}