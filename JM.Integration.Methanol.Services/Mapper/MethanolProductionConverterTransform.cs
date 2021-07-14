using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JM.Integration.Methanol.Services.Mapper
{
    /// <summary>
    /// MethanolProductionConverterTransform : Transformation of mode to API Response
    /// </summary>
    public class MethanolProductionConverterTransform : IMethanolPerConverterTransform
    {
        public string TransactionType { get; set; }

        public MethanolProductionConverterTransform()
        {
            this.TransactionType = "MethanolProductionPerConverter";
        }

        /// <summary>
        /// Tramsform db model to API Response
        /// </summary>
        /// <param name="MethanolProductionPerConverterKPIDataSet"></param>
        /// <returns> BaseKpiResponse </returns>    
        public BaseKpiResponse MethanolPerConverterTransform(IList<MethanolProductionPerConverterKPIDataSet> methanolProductionPerConverterKPIDataSet)
        {
            BaseKpiResponse apiResponse = new BaseKpiResponse();

            foreach (var row in methanolProductionPerConverterKPIDataSet)
            {
                var percent = new BaseKpiDetails();
                var actual = new BaseKpiDetails();

                var baseKPIDataSet = new BaseKpiDataset();

                baseKPIDataSet.Name = row.Name;
                baseKPIDataSet.Url = row.URL;

                percent.Label = row.PercentLabel;
                percent.Id = row.PercentId;
                percent.Value = row.PercentValue;
                percent.Decrement = Convert.ToDouble(row.PercentDecrement);
                percent.Increment = Convert.ToDouble(row.PercentIncrement);
                percent.Unit.Type = row.PercentType;
                percent.Unit.Symbol = row.PercentSymbol;

                actual.Label = row.ActualLabel;
                actual.Id = row.ActualId;
                actual.Value = row.ActualValue;
                actual.Decrement = Convert.ToDouble(row.ActualDecrement);
                actual.Increment = Convert.ToDouble(row.ActualIncrement);
                actual.Unit.Type = row.ActualType;
                actual.Unit.Symbol = row.ActualSymbol;

                baseKPIDataSet.Details.Add(percent);
                baseKPIDataSet.Details.Add(actual);
                apiResponse.DataSet.Add(baseKPIDataSet);
                apiResponse.Title = row.Title;
                apiResponse.Unit = row.Unit;
                apiResponse.MainValue = row.MainValue;
                apiResponse.MaxValue = row.MaxValue;
                apiResponse.Indicator = row.Indicator;
                apiResponse.KpiTitle = row.KpiTitle;
            }
            return apiResponse;
        }
    }
}
