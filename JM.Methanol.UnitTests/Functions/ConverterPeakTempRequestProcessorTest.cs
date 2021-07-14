
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Services.Services;
using JM.Methanol.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JM.Methanol.UnitTests.Functions
{
    public class ConverterPeakTempRequestProcessorTest
    {

        private readonly ConverterPeakTempRequestProcessor _converterPeakTempRequestProcessor;
        private MockBaseDataAccess _mockBaseDataAccess;
        private PeakTempTransform _PeakTempTransform;
        public ConverterPeakTempRequestProcessorTest()
        {
            this._mockBaseDataAccess = new MockBaseDataAccess();
            List<PeakTempTransform> peakTransformList = new List<PeakTempTransform>();
            this._PeakTempTransform = new PeakTempTransform();
            this._PeakTempTransform.TransactionType = "ConverterPeakTemp";
            peakTransformList.Add(_PeakTempTransform);
            this._converterPeakTempRequestProcessor = new ConverterPeakTempRequestProcessor(this._mockBaseDataAccess, peakTransformList);

        }

        [Fact]
        public async Task GetKPIResponse_ValidRequest_Return_ValidNullResponseAsync()
        {
            var output = await this._converterPeakTempRequestProcessor.GetKPIResponse("1");
            Assert.IsType<BaseKpiResponse>(output);
        }
    }
}