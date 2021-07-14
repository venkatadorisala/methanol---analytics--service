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
    /// ConverterPressureDropRequestProcessor
    /// </summary>
    public class ConverterPressureDropRequestProcessor : IRequestProcessor
    {
        private readonly IBaseDataAccess _baseDataAccess;
        private readonly IKPIResponseTransform _converterPressureDropTramsform;

        public ConverterPressureDropRequestProcessor(IBaseDataAccess baseDataAccess, IEnumerable<IKPIResponseTransform> converterPressureDropTramsform)
        {
            this._baseDataAccess = baseDataAccess;
            this.TransactionName = "ConverterPressureDrop";
            this._converterPressureDropTramsform = converterPressureDropTramsform.First(x => x.TransactionType == "ConverterPressureDrop");
        }

        public string TransactionName { get; set; }

        /// <summary>
        /// GetKPIResponse
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<BaseKpiResponse> GetKPIResponse(string requestId)
        {
            var sectionIdparameter = new List<DbParameter>() { _baseDataAccess.GetParameter("@sectionId", requestId) };

            BaseKpiResponse apiResponse = new BaseKpiResponse();
            using (var reader = _baseDataAccess.GetDataReader("[dbo].[USP_GetConverterPressureDropKPIBySectionId]", sectionIdparameter))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["dataset"] == null|| string.IsNullOrWhiteSpace(reader["dataset"].ToString()))
                            return null;
                        apiResponse = this._converterPressureDropTramsform.Transform(JsonConvert.DeserializeObject<List<KPIDbDataSet>>(reader["dataset"].ToString()));
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