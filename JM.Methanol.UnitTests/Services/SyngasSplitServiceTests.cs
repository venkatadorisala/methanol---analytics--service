using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Xunit;

namespace JM.Integration.Methanol.Services.UnitTests
{
    public class SyngasSplitServiceTests
    {
        private const string validSectionId = "1";
        private readonly Mock<IBaseDataAccess> _baseDataAccess = new Mock<IBaseDataAccess>();
        private readonly SyngasSplitService syngasSplitService;

        public SyngasSplitServiceTests()
        {
            syngasSplitService = new SyngasSplitService(_baseDataAccess.Object);
        }

        [Fact]
        [Trait("API", "Syngas Split Service")]
        public async Task Valid_sectionId_returns_SyngasSplit_Object()
        {
            //Arrange
            var mockDataReader = new Mock<DbDataReader>();
            mockDataReader.SetupSequence(c => c.Read()).Returns(true).Returns(false);
            mockDataReader.Setup(x => x.HasRows).Returns(true);
            mockDataReader.Setup(x => x.IsClosed).Returns(false);
            mockDataReader.Setup(x => x["PERCENT_ID"]).Returns("stringValue");
            mockDataReader.Setup(x => x["PERCENT_LABEL"]).Returns("stringValue");
            mockDataReader.Setup(x => x["SYNGAS_FLOW_PERCENT"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["PERCENT_INCREMENT"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["PERCENT_DECREMENT"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["PERCENT_TYPE"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["PERCENT_SYMBOL"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["ACTUAL_ID"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["ACTUAL_LABEL"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["SYNGAS_FLOW_ACT_VALUE"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["ACTUAL_INCREMENT"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["ACTUAL_DECREMENT"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["ACTUAL_TYPE"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["ACTUAL_SYMBOL"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["Converter_Name"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["Convertor_SID"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["SYNGAS_FLOW_ACT_VALUE"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["Converter_Name"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["KPI_Name"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["KPI_TITLE"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["MAINVALUE"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["MAXVALUE"]).Returns(1.123);
            mockDataReader.SetupGet(x => x["UNIT"]).Returns("stringValue");
            mockDataReader.SetupGet(x => x["INDICATOR"]).Returns("stringValue");
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), It.IsAny<CommandType>())).Returns(mockDataReader.Object);
            //Act
            var response = await syngasSplitService.GetSyngasSplitResponse(validSectionId);
            //Assert
            Assert.IsType<SyngasSplit>(response);
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("0123")]
        [InlineData("   ")]
        [InlineData("null")]
        [Trait("API", "Syngas Split Service")]
        public async Task Invalid_sectionId_returns_Empty_Object(string invalidSectionId)
        {
            //Arrange
            DbDataReader dataReader = null;
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), It.IsAny<CommandType>())).Returns(dataReader);
            //Act
            var response = await syngasSplitService.GetSyngasSplitResponse(invalidSectionId);
            //Assert
            Assert.Null(response);
        }
    }
}