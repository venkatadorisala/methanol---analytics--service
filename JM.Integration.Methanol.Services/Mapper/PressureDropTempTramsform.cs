using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;

namespace JM.Integration.Methanol.Services.Mapper
{
    /// <summary>
    /// PressureDropTempTramsform : Transformation of mode to API Response
    /// </summary>

    public class PressureDropTempTramsform : IKPIResponseTransform
    {
        public string TransactionType { get; set; }

        public PressureDropTempTramsform()
        {
            this.TransactionType = "ConverterPressureDrop";
        }

        /// <summary>
        /// Tramsform db model to API Response
        /// </summary>
        /// <param name="kPIDbDataSet"></param>
        /// <returns> BaseKpiResponse </returns>

        public BaseKpiResponse Transform(IList<KPIDbDataSet> kPIDbDataSet)
        {
            BaseKpiResponse apiResponse = new BaseKpiResponse();

            foreach (var row in kPIDbDataSet)
            {
                var analysis = new BaseKpiDetails();
                var normalised4weekavg = new BaseKpiDetails();
                var normalisedLifetimeAvg = new BaseKpiDetails();
                var peak = new BaseKpiDetails();

                var baseKPIDataSet = new BaseKpiDataset();

                baseKPIDataSet.Name = row.Name;
                baseKPIDataSet.Url = row.URL;

                analysis.Label = row.AnalysisLabel;
                analysis.Id = row.AnalysisId;
                analysis.Value = row.AnalysisValue;
                analysis.Decrement = row.AnalysisDecrement;
                analysis.Increment = row.AnalysisIncrement;
                analysis.Unit.Type = row.AnalysisType;
                analysis.Unit.Symbol = row.AnalysisSymbol;

                normalised4weekavg.Label = row.Normalised4weekavgLabel;
                normalised4weekavg.Id = row.Normalised4weekavgId;
                normalised4weekavg.Value = row.Normalised4weekavgValue; 
                normalised4weekavg.Decrement = System.Convert.ToDouble(row.Normalised4weekavgDecrement);
                normalised4weekavg.Increment = System.Convert.ToDouble(row.Normalised4weekavgIncrement);
                normalised4weekavg.Unit.Type = row.Normalised4weekavgType;
                normalised4weekavg.Unit.Symbol = row.Normalised4weekavgSymbol;


             
                normalisedLifetimeAvg.Label = row.NormalisedLifetimeAvgLabel;
                normalisedLifetimeAvg.Id = row.NormalisedLifetimeAvgId;
                normalisedLifetimeAvg.Value = row.NormalisedLifetimeAvgValue.ToString();
                normalisedLifetimeAvg.Decrement = Convert.ToDouble(row.NormalisedLifetimeAvgDecrement);
                normalisedLifetimeAvg.Increment = Convert.ToDouble(row.NormalisedLifetimeAvgIncrement);
                normalisedLifetimeAvg.Unit.Type = row.NormalisedLifetimeAvgType;
                normalisedLifetimeAvg.Unit.Symbol = row.NormalisedLifetimeAvgSymbol;

                baseKPIDataSet.Details.Add(analysis);
                baseKPIDataSet.Details.Add(normalised4weekavg);
                baseKPIDataSet.Details.Add(normalisedLifetimeAvg);
                apiResponse.DataSet.Add(baseKPIDataSet);
                apiResponse.Title = row.Title;
                apiResponse.Unit = row.Unit;
                apiResponse.MaxValue = row.MaxValue;
                apiResponse.Indicator = row.Indicator;
                apiResponse.KpiTitle = row.KpiTitle;
            }

            return apiResponse;
        }
    }
}