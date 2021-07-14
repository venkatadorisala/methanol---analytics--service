using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JM.Integration.Methanol.Services.Mapper
{
    class ReactorPeakTempTransform : IReactorPeakTempTransform
    {
        public string TransactionType { get; set; }

        public ReactorPeakTempTransform()
        {
            this.TransactionType = "ReactorConverterPeakTemp";
        }

        public BaseKpiResponse ReactorConverterPeakTempTransform(IList<ReactorPeakTempKPIDataSet> reactorPeakTempKPIDataSet)
        {
            BaseKpiResponse apiResponse = new BaseKpiResponse();

            foreach (var row in reactorPeakTempKPIDataSet)
            {

                var baseKPIDataSet = new BaseKpiDataset();

                baseKPIDataSet.Name = row.Name;

                apiResponse.DataSet.Add(baseKPIDataSet);
                apiResponse.Title = row.Title;

            }
            return apiResponse;
        }
    }
}
