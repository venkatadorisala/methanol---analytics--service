using JM.Integration.Methanol.DB.Models;
using JM.Integration.Methanol.Services.Models;
using System;

namespace JM.Integration.Methanol.Services.UnitTests.TestHelper
{
    public abstract class ConverterPeakTempTestHelper
    {
        public static BaseKpiResponse BuildConverterPeakTempObject()
        {
            var response = new BaseKpiResponse();

            BaseKpiDetails analysisDetails = new BaseKpiDetails();
            analysisDetails.Id = "ANALYSIS_ID";
            analysisDetails.Label = "ANALYSIS_LABEL";
            analysisDetails.Value = "290.8";
            analysisDetails.Increment = 0;
            analysisDetails.Decrement = 0;
            analysisDetails.Unit.Type = "Celsius";
            analysisDetails.Unit.Symbol = "c";

            BaseKpiDetails peakDetails = new BaseKpiDetails();
            peakDetails.Id = "PEAK_ID";
            peakDetails.Label = "PEAK_LABEL";
            peakDetails.Value = "290.8";
            peakDetails.Increment = 9.3;
            peakDetails.Decrement = 0;
            peakDetails.Unit.Type = "Celsius";
            peakDetails.Unit.Symbol = "c";

            BaseKpiDetails targetMaxDetails = new BaseKpiDetails();
            targetMaxDetails.Id = "TARGET_ID";
            targetMaxDetails.Label = "TARGET_LABEL";
            targetMaxDetails.Value = "315.4";
            targetMaxDetails.Increment = 44.3;
            targetMaxDetails.Decrement = 0;
            targetMaxDetails.Unit.Type = "Celsius";
            targetMaxDetails.Unit.Symbol = "c";

            BaseKpiDetails safetyMaxDetails = new BaseKpiDetails();
            safetyMaxDetails.Id = "SAFETY_ID";
            safetyMaxDetails.Label = "SAFETY_LABEL";
            safetyMaxDetails.Value = "315.4";
            safetyMaxDetails.Increment = 44.3;
            safetyMaxDetails.Decrement = 0;
            safetyMaxDetails.Unit.Type = "Celsius";
            safetyMaxDetails.Unit.Symbol = "c";

            BaseKpiDataset baseKpiDataset = new BaseKpiDataset();
            baseKpiDataset.Name = "Converter_Name";
            baseKpiDataset.Url = "Convertor_SID";
            baseKpiDataset.Details.Add(analysisDetails);
            baseKpiDataset.Details.Add(peakDetails);
            baseKpiDataset.Details.Add(targetMaxDetails);
            baseKpiDataset.Details.Add(safetyMaxDetails);

            BaseDataPoints baseDataPoints = new BaseDataPoints();
            baseDataPoints.Y = 290.8;
            baseDataPoints.Label = "Converter_Name";

            response.DataSet.Add(baseKpiDataset);
            response.Title = "row.Title";
            response.DataPoints.Add(baseDataPoints);
            response.KpiTitle = "KPI_TITLE";
            response.MainValue = Math.Round(13.566, 2);
            response.MaxValue = Math.Round(0.0, 2);
            response.Unit = "UNIT";
            response.Indicator = "INDICATOR";

            return response;
        }
    }
}