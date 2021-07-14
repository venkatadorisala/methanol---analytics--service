using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;

namespace JM.Integration.Methanol.Services.Services
{
    /// <summary>
    /// PeakTempTramsform : Transformation of mode to API Response
    /// </summary>
    public class PeakTempTransform : IPeakTempTransform
    {
        public string TransactionType { get; set; }

        public PeakTempTransform()
        {
            this.TransactionType = "ConverterPeakTemp";
        }

        /// <summary>
        /// Tramsform db model to API Response
        /// </summary>
        /// <param name="converterPeakTempKPIDataSet"></param>
        /// <returns> BaseKpiResponse </returns>
        public BaseKpiResponse ConverterPeakTempTransform(IList<ConverterPeakTempKPIDataSet> converterPeakTempKPIDataSet)
        {
            BaseKpiResponse apiResponse = new BaseKpiResponse();

            foreach (var row in converterPeakTempKPIDataSet)
            {
                var analysis = new BaseKpiDetails();
                var targetMax = new BaseKpiDetails();
                var safetyMax = new BaseKpiDetails();
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

                targetMax.Label = row.TargetMaxLabel;
                targetMax.Id = row.TargetMaxId;
                targetMax.Value = row.TargetMaxValue.ToString(); //# ask for data type change
                targetMax.Decrement = System.Convert.ToDouble(row.TargetMaxDecrement); //# ask for data type change
                targetMax.Increment = System.Convert.ToDouble(row.TargetMaxIncrement.ToString());
                targetMax.Unit.Type = row.TargetMaxType;
                targetMax.Unit.Symbol = row.TargetMaxSymbol;

                safetyMax.Label = row.SafetyMaxLabel;
                safetyMax.Id = row.SafetyMaxId;
                safetyMax.Value = row.SafetyMaxValue.ToString();
                safetyMax.Decrement = Convert.ToDouble(row.SafetyMaxDecrement);
                safetyMax.Increment = Convert.ToDouble(row.SafetyMaxIncrement);
                safetyMax.Unit.Type = row.SafetyMaxType;
                safetyMax.Unit.Symbol = row.SafetyMaxSymbol;

                peak.Label = row.PeakLabel;
                peak.Id = row.PeakId;
                peak.Value = row.peakValue;
                peak.Decrement = Convert.ToDouble(row.PeakDecrement);
                peak.Increment = Convert.ToDouble(row.PeakIncrement);
                peak.Unit.Type = row.PeakType;
                peak.Unit.Symbol = row.PeakSymbol;

                baseKPIDataSet.Details.Add(analysis);
                baseKPIDataSet.Details.Add(targetMax);
                baseKPIDataSet.Details.Add(safetyMax);
                baseKPIDataSet.Details.Add(peak);
                apiResponse.DataSet.Add(baseKPIDataSet);
                apiResponse.Title = row.Title;
            }

            return apiResponse;
        }
    }
}