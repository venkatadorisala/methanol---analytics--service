using Bogus;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Mapper;
using JM.Integration.Methanol.Services.Models;
using System.Collections.Generic;
using Xunit;

namespace JM.Methanol.UnitTests.Services
{
    public class PressureDropTempTramsformTest
    {
        private readonly PressureDropTempTramsform pressureDropTransform;
        private readonly IList<KPIDbDataSet> kPIDbDatasetList;
        private readonly KPIDbDataSet fakeKPIDbDataSet;

        public PressureDropTempTramsformTest()
        {
            this.kPIDbDatasetList = new List<KPIDbDataSet>();

            this.pressureDropTransform = new PressureDropTempTramsform();
            this.fakeKPIDbDataSet = new Faker<KPIDbDataSet>();
        }

        [Fact]
        public void TramsformTOBaseKPIResponse_ForEmptyRequest_EmptyResponse()
        {
            BaseKpiResponse baseKpiResponse = pressureDropTransform.Transform(this.kPIDbDatasetList);
            Assert.Equal(baseKpiResponse.Title, null);
            Assert.Equal(baseKpiResponse.KpiTitle, null);
            Assert.Equal(baseKpiResponse.DataPoints.Count, 0);
            Assert.Equal(baseKpiResponse.DataSet.Count, 0);
        }

        [Fact]
        public void PeakTempTramsformTOBaseKPIResponse_ForValidRequest_ValidResponse()
        {
            this.fakeKPIDbDataSet.Normalised4weekavgDecrement = 1;
            this.fakeKPIDbDataSet.Normalised4weekavgValue = 55.ToString();
            this.fakeKPIDbDataSet.Normalised4weekavgIncrement = 1;
            this.kPIDbDatasetList.Add(this.fakeKPIDbDataSet);

            BaseKpiResponse baseKpiResponse = pressureDropTransform.Transform(this.kPIDbDatasetList);
            Assert.Equal(baseKpiResponse.Title, fakeKPIDbDataSet.Title);
            Assert.Equal(baseKpiResponse.DataSet[0].Name, fakeKPIDbDataSet.Name);
            Assert.Equal(baseKpiResponse.DataSet[0].Url, fakeKPIDbDataSet.URL);
        }
    }
}