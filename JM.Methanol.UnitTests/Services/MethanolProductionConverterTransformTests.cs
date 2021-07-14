using Bogus;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Mapper;
using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JM.Methanol.UnitTests.Services
{
    public class MethanolProductionConverterTransformTests
    {

        private readonly MethanolProductionConverterTransform objMethanolProductionConverterTransform;
        private readonly IList<MethanolProductionPerConverterKPIDataSet> methanolProductionPerConverterKPIDataSetList;
        private readonly MethanolProductionPerConverterKPIDataSet fakeMethanolProductionModel;

        public MethanolProductionConverterTransformTests()
        {
            this.methanolProductionPerConverterKPIDataSetList = new List<MethanolProductionPerConverterKPIDataSet>();
            this.objMethanolProductionConverterTransform = new MethanolProductionConverterTransform();
            this.fakeMethanolProductionModel = new Faker<MethanolProductionPerConverterKPIDataSet>();
        }

        [Fact]
        public void MethanolProductionConverterTransformTOBaseKPIResponse_ForEmptyRequest_EmptyResponse()
        {
            BaseKpiResponse baseKpiResponse = objMethanolProductionConverterTransform.MethanolPerConverterTransform(methanolProductionPerConverterKPIDataSetList);
            Assert.Equal(baseKpiResponse.Title, null);
            Assert.Equal(baseKpiResponse.KpiTitle, null);
            Assert.Equal(baseKpiResponse.DataPoints.Count, 0);
            Assert.Equal(baseKpiResponse.DataSet.Count, 0);
        }

        [Fact]
        public void MethanolProductionConverterTransformTOBaseKPIResponse_ForValidRequest_ValidResponse()
        {
            this.methanolProductionPerConverterKPIDataSetList.Add(this.fakeMethanolProductionModel);

            BaseKpiResponse baseKpiResponse = objMethanolProductionConverterTransform.MethanolPerConverterTransform(methanolProductionPerConverterKPIDataSetList);
            Assert.Equal(baseKpiResponse.Title, fakeMethanolProductionModel.Title);
            Assert.Equal(baseKpiResponse.DataSet[0].Name, fakeMethanolProductionModel.Name);
            Assert.Equal(baseKpiResponse.DataSet[0].Url, fakeMethanolProductionModel.URL);
        }

    }

}
