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
    /// <summary>
    /// MethanolProductionPerConverterProcessor
    /// </summary>
    public class MethanolProductionPerConverterProcessor : IRequestProcessor
    {
        private readonly IBaseDataAccess _baseDataAccess;
        private readonly IMethanolPerConverterTransform _methanolProductionConverterTransform;

        public MethanolProductionPerConverterProcessor(IBaseDataAccess baseDataAccess, IEnumerable<IMethanolPerConverterTransform> methanolProductionConverterTransform)
        {
            this._baseDataAccess = baseDataAccess;
            this.TransactionName = "MethanolProductionPerConverter";
            this._methanolProductionConverterTransform = methanolProductionConverterTransform.First(x => x.TransactionType == "MethanolProductionPerConverter");
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
        public async Task<BaseKpiResponse> GetKPIResponse(string requestId)
        {
            var sectionIdparameter = new List<DbParameter>() { _baseDataAccess.GetParameter("@sectionId", requestId) };
            BaseKpiResponse apiResponse = new BaseKpiResponse();

            using (var reader = _baseDataAccess.GetDataReader("[dbo].[USP_GetMethanolProductionKPIBySectionId]", sectionIdparameter))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["dataset"] == null || string.IsNullOrWhiteSpace(reader["dataset"].ToString()))
                             return null;

                        apiResponse = this._methanolProductionConverterTransform.MethanolPerConverterTransform(JsonConvert.DeserializeObject<List<MethanolProductionPerConverterKPIDataSet>>(reader["dataset"].ToString()));
                        var dataPoints = JsonConvert.DeserializeObject<List<BaseDataPoints>>(reader["datapoint"].ToString());
                        apiResponse.Title = reader["title"].ToString();                     
                        apiResponse.DataPoints = dataPoints;
                    }
                    await reader.CloseAsync().ConfigureAwait(true);
                }
                return apiResponse;
            }
        }
    }
}
