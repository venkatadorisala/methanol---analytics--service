using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using JM.Methanol.UnitTests.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JM.Methanol.UnitTests.Services
{
    public class MethanolProductionPerConverterProcessorTests
    {
        private const string validSectionId = "1";

        private readonly Mock<IMethanolPerConverterTransform> _methanolProductionConverterTransform = new Mock<IMethanolPerConverterTransform>();
        private readonly MethanolProductionPerConverterProcessor _methanolProductionPerConverterProcessor;
        private readonly Mock<IBaseDataAccess> _baseDataAccess = new Mock<IBaseDataAccess>();

        public MethanolProductionPerConverterProcessorTests()
        {
            var _mockMethanolTransform = new List<IMethanolPerConverterTransform>();
            _methanolProductionConverterTransform.SetupProperty(x => x.TransactionType, "MethanolProductionPerConverter");
            _mockMethanolTransform.Add(_methanolProductionConverterTransform.Object);
            _methanolProductionPerConverterProcessor = new MethanolProductionPerConverterProcessor(_baseDataAccess.Object, _mockMethanolTransform);

        }

        [Fact]
        [Trait("API", "Methanol Production PerConverter")]
        public async Task Valid_section_id_returns_valid_methanol_perconverter_response()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var response = MethanolProductionPerConverterHelper.BuildConverterPeakTempObject();
            var mockDataReader = new Mock<DbDataReader>();
            mockDataReader.SetupSequence(c => c.Read()).Returns(true).Returns(false);
            mockDataReader.Setup(x => x.HasRows).Returns(true);
            mockDataReader.Setup(x => x.IsClosed).Returns(false);
            mockDataReader.Setup(x => x["dataset"]).Returns(JsonConvert.SerializeObject(response.DataSet));
            mockDataReader.Setup(x => x["datapoint"]).Returns(JsonConvert.SerializeObject(response.DataPoints));
            mockDataReader.Setup(x => x["title"]).Returns(JsonConvert.SerializeObject(response.Title));
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), System.Data.CommandType.StoredProcedure)).Returns(mockDataReader.Object);
            _methanolProductionConverterTransform.Setup(x => x.MethanolPerConverterTransform(It.IsAny<IList<MethanolProductionPerConverterKPIDataSet>>())).Returns(response);
            //Act
            var result = await _methanolProductionPerConverterProcessor.GetKPIResponse(validSectionId);
            //Assert
            Assert.IsType<BaseKpiResponse>(result);
        }

        [Fact]
        [Trait("API", "Methanol Production PerConverter")]
        public async Task InValid_section_id_returns_empty_methanol_perconverter_response()
        {
            //Arrange
            DbDataReader dataReader = null;
            _baseDataAccess.Setup(x => x.GetDataReader(It.IsAny<string>(), It.IsAny<List<DbParameter>>(), System.Data.CommandType.StoredProcedure)).Returns(dataReader);
            //Act
            var result = await _methanolProductionPerConverterProcessor.GetKPIResponse(validSectionId);
            //Assert
            Assert.Null(result.KpiTitle);
            Assert.Empty(result.DataSet);
            Assert.Empty(result.DataPoints);
        }
    }
}
