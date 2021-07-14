using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.Helpers;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace JM.Integration.Methanol.Services.Services
{
    /// <summary>
    /// <inheritdoc cref="ISyngasSplitService"/>
    /// </summary>
    public class SyngasSplitService : ISyngasSplitService
    {
        private readonly IBaseDataAccess _baseDataAccess;

        /// <summary>
        /// Initializes new instance of <see cref="SyngasSplitService"/>
        /// </summary>
        /// <param name="baseDataAccess"></param>
        public SyngasSplitService(IBaseDataAccess baseDataAccess)
        {
            _baseDataAccess = baseDataAccess;
        }

        public async Task<SyngasSplit> GetSyngasSplitResponse(string sectionId)
        {
            SyngasSplit response = null;
            double output = 0;
            if (!string.IsNullOrWhiteSpace(sectionId))
            {
                var sectionIdparameter = new List<DbParameter>() { _baseDataAccess.GetParameter("@sectionId", sectionId) };
                using (var reader = _baseDataAccess.GetDataReader("[dbo].[usp_SyngasSplit]", sectionIdparameter))
                {
                    if (reader != null && reader.HasRows)
                    {
                        response = new SyngasSplit();
                        while (reader.Read())
                        {
                            BaseKpiDetails percentDetails = new BaseKpiDetails();
                            percentDetails.Id = DataHelper.ConvertStringValue(reader, "PERCENT_ID");
                            percentDetails.Label = DataHelper.ConvertStringValue(reader, "PERCENT_LABEL");
                            percentDetails.Value = DataHelper.ConvertStringValue(reader, "SYNGAS_FLOW_PERCENT");
                            output = DataHelper.ConvertDoubleValue(reader, "PERCENT_INCREMENT");
                            percentDetails.Increment = Math.Round(output, 2);
                            output = DataHelper.ConvertDoubleValue(reader, "PERCENT_DECREMENT");
                            percentDetails.Decrement = Math.Round(output, 2);
                            percentDetails.Unit.Type = DataHelper.ConvertStringValue(reader, "PERCENT_TYPE");
                            percentDetails.Unit.Symbol = DataHelper.ConvertStringValue(reader, "PERCENT_SYMBOL");

                            BaseKpiDetails actualDetails = new BaseKpiDetails();
                            actualDetails.Id = DataHelper.ConvertStringValue(reader, "ACTUAL_ID");
                            actualDetails.Label = DataHelper.ConvertStringValue(reader, "ACTUAL_LABEL");
                            actualDetails.Value = DataHelper.ConvertStringValue(reader, "SYNGAS_FLOW_ACT_VALUE");

                            output = DataHelper.ConvertDoubleValue(reader, "ACTUAL_INCREMENT");
                            actualDetails.Increment = Math.Round(output, 2);

                            output = DataHelper.ConvertDoubleValue(reader, "ACTUAL_DECREMENT");
                            actualDetails.Decrement = Math.Round(output, 2);

                            actualDetails.Unit.Type = DataHelper.ConvertStringValue(reader, "ACTUAL_TYPE");
                            actualDetails.Unit.Symbol = DataHelper.ConvertStringValue(reader, "ACTUAL_SYMBOL");

                            BaseKpiDataset baseKpiDataset = new BaseKpiDataset();
                            baseKpiDataset.Name = DataHelper.ConvertStringValue(reader, "Converter_Name");
                            baseKpiDataset.Url = DataHelper.ConvertStringValue(reader, "Converter_SID");

                            baseKpiDataset.Details.Add(percentDetails);
                            baseKpiDataset.Details.Add(actualDetails);

                            BaseDataPoints baseDataPoints = new BaseDataPoints();
                            output = DataHelper.ConvertDoubleValue(reader, "SYNGAS_FLOW_ACT_VALUE");
                            baseDataPoints.Y = Math.Round(output, 2);
                            baseDataPoints.Label = DataHelper.ConvertStringValue(reader, "Converter_Name");

                            response.DataSet.Add(baseKpiDataset);
                            response.Title = DataHelper.ConvertStringValue(reader, "KPI_Name");
                            response.DataPoints.Add(baseDataPoints);
                            response.KpiTitle = DataHelper.ConvertStringValue(reader, "KPI_TITLE");

                            output = DataHelper.ConvertDoubleValue(reader, "MAINVALUE");
                            response.MainValue = Math.Round(output, 2);

                            output = DataHelper.ConvertDoubleValue(reader, "MAXVALUE");
                            response.MaxValue = Math.Round(output, 2);
                            response.Unit = DataHelper.ConvertStringValue(reader, "UNIT");
                            response.Indicator = DataHelper.ConvertStringValue(reader, "INDICATOR");
                        }
                        await reader.CloseAsync().ConfigureAwait(true);
                    }
                }
            }

            return response;
        }
    }
}