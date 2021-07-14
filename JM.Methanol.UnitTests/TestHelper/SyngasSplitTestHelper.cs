using JM.Integration.Methanol.DB.Models;
using JM.Integration.Methanol.Services.Models;
using System;

namespace JM.Integration.Methanol.Services.UnitTests.TestHelper
{
    public abstract class SyngasSplitTestHelper
    {
        public static SyngasSplit BuildSyngasSplitObject()
        {
            var response = new SyngasSplit();

            BaseKpiDetails percentDetails = new BaseKpiDetails();
            percentDetails.Id = "PERCENT_ID";
            percentDetails.Label = "PERCENT_LABEL";
            percentDetails.Value = "20.65";
            percentDetails.Increment = 0;
            percentDetails.Decrement = 0;
            percentDetails.Unit.Type = "%";
            percentDetails.Unit.Symbol = "%";

            BaseKpiDetails actualDetails = new BaseKpiDetails();
            actualDetails.Id = "ACTUAL_ID";
            actualDetails.Label = "ACTUAL_LABEL";
            actualDetails.Value = "54.36";
            actualDetails.Increment = 0;
            actualDetails.Decrement = 0;
            actualDetails.Unit.Type = "";
            actualDetails.Unit.Symbol = "";

            BaseKpiDataset baseKpiDataset = new BaseKpiDataset();
            baseKpiDataset.Name = "Converter_Name";
            baseKpiDataset.Url = "Convertor_SID";
            baseKpiDataset.Details.Add(percentDetails);
            baseKpiDataset.Details.Add(actualDetails);

            BaseDataPoints baseDataPoints = new BaseDataPoints();
            baseDataPoints.Y = 54.63;
            baseDataPoints.Label = "Converter_Name";

            response.DataSet.Add(baseKpiDataset);
            response.Title = "KPI_Name";
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