using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using JM.Integration.Methanol.Services.UnitTests.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Xunit;

namespace JM.Methanol.UnitTests.Services
{
    public class ConverterPeakTempRequestProcessorTests
    {
        private const string validSectionId = "1";

        private readonly Mock<IPeakTempTransform> _peakTempTransform = new Mock<IPeakTempTransform>();
        private readonly ConverterPeakTempRequestProcessor _converterPeakTempRequestProcessor;
        private readonly Mock<IBaseDataAccess> _baseDataAccess = new Mock<IBaseDataAccess>();

        public ConverterPeakTempRequestProcessorTests()
        {
            var _mockPeakTransform = new List<IPeakTempTransform>();
            _peakTempTransform.SetupProperty(x => x.TransactionType, "ConverterPeakTemp");
            _mockPeakTransform.Add(_peakTempTransform.Object);
            _converterPeakTempRequestProcessor = new ConverterPeakTempRequestProcessor(_baseDataAccess.Object, _mockPeakTransform);
        }

        [Fact]
        [Trait("API", "Converter Peak Temp")]
        public async Task Valid_section_id_returns_valid_peak_temp_response()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var response = ConverterPeakTempTestHelper.BuildConverterPeakTempObject();
            var mockDataReader = new Mock<DbDataReader>();
            mockDataReader.SetupSequence(c => c.Read()).Returns(true).Returns(false);
            mockDataReader.Setup(x => x.HasRows).Returns(true);
            mockDataReader.Setup(x => x.IsClosed).Returns(false);
            mockDataReader.Setup(x => x["dataset"]).Returns(JsonConvert.SerializeObject(response.DataSet));
            mockDataReader.Setup(x => x["datapoint"]).Returns(JsonConvert.SerializeObject(response.DataPoints));
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), System.Data.CommandType.StoredProcedure)).Returns(mockDataReader.Object);
            _peakTempTransform.Setup(x => x.ConverterPeakTempTransform(It.IsAny<IList<ConverterPeakTempKPIDataSet>>())).Returns(response);
            //Act
            var result = await _converterPeakTempRequestProcessor.GetKPIResponse(validSectionId);
            //Assert
            Assert.IsType<BaseKpiResponse>(result);
        }

        [Fact]
        [Trait("API", "Converter Peak Temp")]
        public async Task InValid_section_id_returns_empty_peak_temp_response()
        {
            //Arrange
            DbDataReader dataReader = null;
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), System.Data.CommandType.StoredProcedure)).Returns(dataReader);
            //Act
            var result = await _converterPeakTempRequestProcessor.GetKPIResponse(validSectionId);
            //Assert
            Assert.Null(result.KpiTitle);
            Assert.Empty(result.DataSet);
            Assert.Empty(result.DataPoints);
        }


    }
}
