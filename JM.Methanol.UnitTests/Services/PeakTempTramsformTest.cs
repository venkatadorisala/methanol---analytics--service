using Bogus;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using System.Collections.Generic;
using Xunit;

namespace JM.Methanol.UnitTests.Services
{
    /// <summary>
    /// PeakTempTramsformTest
    /// </summary>
    public class PeakTempTramsformTest
    {
        private readonly IPeakTempTransform objPeakTempTransform;
        private readonly IList<ConverterPeakTempKPIDataSet> converterPeakTempKPIDataSetList;
        private readonly ConverterPeakTempKPIDataSet fakeConverterPeakModel;

        public PeakTempTramsformTest()
        {
            this.converterPeakTempKPIDataSetList = new List<ConverterPeakTempKPIDataSet>();

            this.objPeakTempTransform = new PeakTempTransform();
            this.fakeConverterPeakModel = new Faker<ConverterPeakTempKPIDataSet>();
        }

        [Fact]
        public void PeakTempTramsformTOBaseKPIResponse_ForEmptyRequest_EmptyResponse()
        {
            BaseKpiResponse baseKpiResponse = objPeakTempTransform.ConverterPeakTempTransform(converterPeakTempKPIDataSetList);
            Assert.Equal(baseKpiResponse.Title, null);
            Assert.Equal(baseKpiResponse.KpiTitle, null);
            Assert.Equal(baseKpiResponse.DataPoints.Count, 0);
            Assert.Equal(baseKpiResponse.DataSet.Count, 0);
        }

        [Fact]
        public void PeakTempTramsformTOBaseKPIResponse_ForValidRequest_ValidResponse()
        {
            this.converterPeakTempKPIDataSetList.Add(this.fakeConverterPeakModel);

            BaseKpiResponse baseKpiResponse = objPeakTempTransform.ConverterPeakTempTransform(converterPeakTempKPIDataSetList);
            Assert.Equal(baseKpiResponse.Title, fakeConverterPeakModel.Title);
            Assert.Equal(baseKpiResponse.DataSet[0].Name, fakeConverterPeakModel.Name);
            Assert.Equal(baseKpiResponse.DataSet[0].Url, fakeConverterPeakModel.URL);
        }
    }
}