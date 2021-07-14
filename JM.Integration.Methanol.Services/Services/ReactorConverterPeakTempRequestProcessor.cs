using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Integration.Methanol.Services.Services
{
    public class ReactorConverterPeakTempRequestProcessor : IRequestProcessor
    {
        private readonly IBaseDataAccess _baseDataAccess;
        private readonly IReactorPeakTempTransform _reactorPeakTempTransform;

        public ReactorConverterPeakTempRequestProcessor(IBaseDataAccess baseDataAccess, IEnumerable<IReactorPeakTempTransform> reactorPeakTempTransform)
        {
            this._baseDataAccess = baseDataAccess;
            this.TransactionName = "ReactorConverterPeakTemp";
            this._reactorPeakTempTransform = reactorPeakTempTransform.First(x => x.TransactionType == "ReactorConverterPeakTemp");
        }

        public string TransactionName { get; set; }

        public async Task<BaseKpiResponse> GetKPIResponse(string requestId)
        {
            var reactorIdparameter = new List<DbParameter>() { _baseDataAccess.GetParameter("@reactorId", requestId) }; //-----

            BaseKpiResponse apiResponse = new BaseKpiResponse();

            using (var reader = _baseDataAccess.GetDataReader("[dbo].[USP_GetConverterPeakTempKPIBySectionId]", reactorIdparameter))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["dataset"] == null || string.IsNullOrWhiteSpace(reader["dataset"].ToString()))
                            return null;
                        apiResponse = this._reactorPeakTempTransform.ReactorConverterPeakTempTransform(JsonConvert.DeserializeObject<List<ReactorPeakTempKPIDataSet>>(reader["dataset"].ToString()));
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
