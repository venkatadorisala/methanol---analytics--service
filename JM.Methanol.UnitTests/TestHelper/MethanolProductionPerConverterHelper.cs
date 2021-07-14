using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JM.Methanol.UnitTests.TestHelper
{
    public abstract class MethanolProductionPerConverterHelper
    {
        public static BaseKpiResponse BuildConverterPeakTempObject()
        {
            var response = new BaseKpiResponse();

            BaseKpiDetails percentDetails = new BaseKpiDetails();
            percentDetails.Id = "PERCENT_ID";
            percentDetails.Label = "PERCENT_LABEL";
            percentDetails.Value = "52";
            percentDetails.Increment = 0;
            percentDetails.Decrement = 0;
            percentDetails.Unit.Type = "%";
            percentDetails.Unit.Symbol = "%";

            BaseKpiDetails actualDetails = new BaseKpiDetails();
            actualDetails.Id = "ACTUAL_ID";
            actualDetails.Label = "ACTUAL_LABEL";
            actualDetails.Value = "1230";
            actualDetails.Increment = 0;
            actualDetails.Decrement = 0;
            actualDetails.Unit.Type = "";
            actualDetails.Unit.Symbol = "";

            BaseDataPoints baseDataPoints = new BaseDataPoints();
            baseDataPoints.Y = 1230;
            baseDataPoints.Label = "Converter_Name";

            BaseKpiDataset baseKpiDataset = new BaseKpiDataset();
            baseKpiDataset.Name = "Converter_Name";
            baseKpiDataset.Url = "Convertor_SID";
            baseKpiDataset.Details.Add(percentDetails);
            baseKpiDataset.Details.Add(actualDetails);
           

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
