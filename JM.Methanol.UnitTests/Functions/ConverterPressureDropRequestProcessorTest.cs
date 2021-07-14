using Bogus;
using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Mapper;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using JM.Methanol.UnitTests.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JM.Methanol.UnitTests.Functions
{
    public class ConverterPressureDropRequestProcessorTest
    {

        private readonly MockBaseDataAccess mockBaseDataAccess;
        private readonly ConverterPressureDropRequestProcessor converterPressureDropRequestProcessor;
        private readonly PressureDropTempTramsform pressureDropTransform;
        private readonly IKPIResponseTransform kPIResponseTransform;
        private readonly KPIDbDataSet fakekPIDbDataSets;
        private readonly List<PressureDropTempTramsform> KPIResponseTransformList;
        private readonly BaseKpiResponse baseKpiResponse;

        private readonly Mock<PressureDropTempTramsform> mockPressureDropTempTransform;


        public ConverterPressureDropRequestProcessorTest()
        {

            this.mockBaseDataAccess = new MockBaseDataAccess();

            this.KPIResponseTransformList = new List<PressureDropTempTramsform>();
            this.pressureDropTransform = new PressureDropTempTramsform();
            this.pressureDropTransform.TransactionType = "ConverterPressureDrop";
            this.KPIResponseTransformList.Add(pressureDropTransform);
            this.mockPressureDropTempTransform = new Mock<PressureDropTempTramsform>();
            this.baseKpiResponse = new BaseKpiResponse();
            this.converterPressureDropRequestProcessor = new ConverterPressureDropRequestProcessor(this.mockBaseDataAccess, this.KPIResponseTransformList);
    
            this.fakekPIDbDataSets = new Faker<KPIDbDataSet>();
      
        }


        [Fact]
        public async Task GetKPIResponse_ValidRequest_Return_ValidNullResponseAsync()
        {

            var output = await this.converterPressureDropRequestProcessor.GetKPIResponse("1");
             Assert.IsType<BaseKpiResponse>(output);
            

        }



    }
}
