// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

using Newtonsoft.Json;

namespace JM.Integration.Methanol.Common.DBModel
{
    /// <summary>
    /// ConverterPressureDropKPIDataSet
    /// </summary>
    public class ConverterPressureDropKPIDataSet
    {
        /// <summary>
        /// Gets or sets title.
        /// </summary>
        [JsonProperty("title")]
        public string Title;

        /// <summary>
        /// Gets or sets name
        /// </summary>
        [JsonProperty("name")]
        public string Name;

        /// <summary>
        /// Gets or sets url
        /// </summary>
        [JsonProperty("url")]
        public string URL;

        /// <summary>
        /// Gets or sets analysisLabel
        /// </summary>
        [JsonProperty("analysisLabel")]
        public string AnalysisLabel;

        /// <summary>
        /// Gets or sets analysisValue
        /// </summary>
        [JsonProperty("analysisValue")]
        public string AnalysisValue;

        /// <summary>
        /// Gets or sets analysisIncrement
        /// </summary>
        [JsonProperty("analysisIncrement")]
        public float AnalysisIncrement;

        /// <summary>
        /// Gets or sets analysisDecrement
        /// </summary>
        [JsonProperty("analysisDecrement")]
        public float AnalysisDecrement;

        /// <summary>
        /// Gets or sets analysisType
        /// </summary>
        [JsonProperty("analysisType")]
        public string AnalysisType;

        /// <summary>
        /// Gets or sets analysisSymbol
        /// </summary>
        [JsonProperty("analysisSymbol")]
        public string AnalysisSymbol;

        /// <summary>
        /// Gets or sets analysisId
        /// </summary>
        [JsonProperty("analysisId")]
        public string AnalysisId;

        /// <summary>
        /// Gets or sets normalised4weekavgLabel
        /// </summary>
        [JsonProperty("normalised4weekavgLabel")]
        public string Normalised4weekavgLabel;

        /// <summary>
        /// Gets or sets normalised4weekavgValue
        /// </summary>
        [JsonProperty("normalised4weekavgValue")]
        public string Normalised4weekavgValue;

        /// <summary>
        /// Gets or sets normalised4weekavgIncrement
        /// </summary>
        [JsonProperty("normalised4weekavgIncrement")]
        public float Normalised4weekavgIncrement;

        /// <summary>
        /// Gets or sets normalised4weekavgDecrement
        /// </summary>
        [JsonProperty("normalised4weekavgDecrement")]
        public float Normalised4weekavgDecrement;

        /// <summary>
        /// Gets or sets normalised4weekavgType
        /// </summary>
        [JsonProperty("normalised4weekavgType")]
        public string Normalised4weekavgType;

        /// <summary>
        /// Gets or sets normalised4weekavgSymbol
        /// </summary>
        [JsonProperty("normalised4weekavgSymbol")]
        public string Normalised4weekavgSymbol;

        /// <summary>
        /// Gets or sets normalised4weekavgId
        /// </summary>
        [JsonProperty("normalised4weekavgId")]
        public string Normalised4weekavgId;

        /// <summary>
        /// Gets or sets normalisedLifetimeAvgId
        /// </summary>
        [JsonProperty("normalisedLifetimeAvgId")]
        public string NormalisedLifetimeAvgId;

        /// <summary>
        /// Gets or sets normalisedLifetimeAvgLabel
        /// </summary>
        [JsonProperty("normalisedLifetimeAvgLabel")]
        public string NormalisedLifetimeAvgLabel;

        /// <summary>
        /// Gets or sets normalisedLifetimeAvgValue
        /// </summary>
        [JsonProperty("normalisedLifetimeAvgValue")]
        public float NormalisedLifetimeAvgValue;

        /// <summary>
        /// Gets or sets normalisedLifetimeAvgIncrement
        /// </summary>
        [JsonProperty("normalisedLifetimeAvgIncrement")]
        public float NormalisedLifetimeAvgIncrement;

        /// <summary>
        /// Gets or sets normalisedLifetimeAvgDecrement
        /// </summary>
        [JsonProperty("normalisedLifetimeAvgDecrement")]
        public float NormalisedLifetimeAvgDecrement;

        /// <summary>
        /// Gets or sets normalisedLifetimeAvgType
        /// </summary>
        [JsonProperty("normalisedLifetimeAvgType")]
        public string NormalisedLifetimeAvgType;

        /// <summary>
        /// Gets or sets normalisedLifetimeAvgSymbol
        /// </summary>
        [JsonProperty("normalisedLifetimeAvgSymbol")]
        public string NormalisedLifetimeAvgSymbol;

        /// <summary>
        /// Gets or sets  kpiTitle
        /// </summary>
        [JsonProperty("kpiTitle")]
        public string KpiTitle;

        /// <summary>
        /// Gets or sets  maxValue
        /// </summary>
        [JsonProperty("maxValue")]
        public float MaxValue;

        /// <summary>
        /// Gets or sets  unit
        /// </summary>
        [JsonProperty("unit")]
        public string Unit;

        /// <summary>
        /// Gets or sets  indicator
        /// </summary>
        [JsonProperty("indicator")]
        public string Indicator;
    }
}