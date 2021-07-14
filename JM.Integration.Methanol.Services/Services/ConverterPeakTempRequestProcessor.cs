using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace JM.Integration.Methanol.Services.Services
{
    /// <summary>
    /// ConverterPeakTempRequestProcessor
    /// </summary>
    public class ConverterPeakTempRequestProcessor : IRequestProcessor
    {
        private readonly IBaseDataAccess _baseDataAccess;
        private readonly IPeakTempTransform _peakTempTransform;

        public ConverterPeakTempRequestProcessor(IBaseDataAccess baseDataAccess, IEnumerable<IPeakTempTransform> peakTempTransform)
        {
            this._baseDataAccess = baseDataAccess;
            this.TransactionName = "ConverterPeakTemp";
            this._peakTempTransform = peakTempTransform.First(x => x.TransactionType == "ConverterPeakTemp");
        }
        
        /// <summary>
        /// Type of KPI transaction
        /// </summary>      
        public string TransactionName { get; set; }

        /// <summary>
        /// GetKPIResponse
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        ///
        public async Task<BaseKpiResponse> GetKPIResponse(string requestId)
        {
            var sectionIdparameter = new List<DbParameter>() { _baseDataAccess.GetParameter("@sectionId", requestId) };

            BaseKpiResponse apiResponse = new BaseKpiResponse();
            using (var reader = _baseDataAccess.GetDataReader("[dbo].[USP_GetConverterPeakTempKPIBySectionId]", sectionIdparameter))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["dataset"] == null || string.IsNullOrWhiteSpace(reader["dataset"].ToString()))
                            return null;
                        apiResponse = this._peakTempTransform.ConverterPeakTempTransform(JsonConvert.DeserializeObject<List<ConverterPeakTempKPIDataSet>>(reader["dataset"].ToString()));
                        var dataPoints = JsonConvert.DeserializeObject<List<BaseDataPoints>>(reader["datapoint"].ToString());
                        apiResponse.DataPoints = dataPoints;
                    }
                    await reader.CloseAsync().ConfigureAwait(true);
                }
                return apiResponse;
            }
        }
    }
}