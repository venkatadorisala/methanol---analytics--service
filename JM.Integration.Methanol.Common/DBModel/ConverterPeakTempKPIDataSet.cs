// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

using Newtonsoft.Json;

namespace JM.Integration.Methanol.Common.DBModel
{
    /// <summary>
    /// ConverterPeakTempKPIDataSet
    /// </summary>
    public class ConverterPeakTempKPIDataSet
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
        /// Gets or sets peakLabel
        /// </summary>
        [JsonProperty("peakLabel")]
        public string PeakLabel;

        /// <summary>
        /// Gets or sets peakValue
        /// </summary>
        [JsonProperty("peakValue")]
        public string peakValue;

        /// <summary>
        /// Gets or sets peakIncrement
        /// </summary>
        [JsonProperty("peakIncrement")]
        public float PeakIncrement;

        /// <summary>
        /// Gets or sets peakDecrement
        /// </summary>
        [JsonProperty("peakDecrement")]
        public float PeakDecrement;

        /// <summary>
        /// Gets or sets peakType
        /// </summary>
        [JsonProperty("peakType")]
        public string PeakType;

        /// <summary>
        /// Gets or sets peakSymbol
        /// </summary>
        [JsonProperty("peakSymbol")]
        public string PeakSymbol;

        /// <summary>
        /// Gets or sets peakId
        /// </summary>
        [JsonProperty("peakId")]
        public string PeakId;

        /// <summary>
        /// Gets or sets targetMaxId
        /// </summary>
        [JsonProperty("targetMaxId")]
        public string TargetMaxId;

        /// <summary>
        /// Gets or sets targetMaxLabel
        /// </summary>
        [JsonProperty("targetMaxLabel")]
        public string TargetMaxLabel;

        /// <summary>
        /// Gets or sets targetMaxValue
        /// </summary>
        [JsonProperty("targetMaxValue")]
        public float TargetMaxValue;

        /// <summary>
        /// Gets or sets targetMaxIncrement
        /// </summary>
        [JsonProperty("targetMaxIncrement")]
        public float TargetMaxIncrement;

        /// <summary>
        /// Gets or sets targetMaxDecrement
        /// </summary>
        [JsonProperty("targetMaxDecrement")]
        public float TargetMaxDecrement;

        /// <summary>
        /// Gets or sets targetMaxType
        /// </summary>
        [JsonProperty("targetMaxType")]
        public string TargetMaxType;

        /// <summary>
        /// Gets or sets targetMaxSymbol
        /// </summary>
        [JsonProperty("targetMaxSymbol")]
        public string TargetMaxSymbol;

        /// <summary>
        /// Gets or sets  safetyMaxId
        /// </summary>
        [JsonProperty("safetyMaxId")]
        public string SafetyMaxId;

        /// <summary>
        /// Gets or sets  safetyMaxLabel
        /// </summary>
        [JsonProperty("safetyMaxLabel")]
        public string SafetyMaxLabel;

        /// <summary>
        /// Gets or sets  safetyMaxValue
        /// </summary>
        [JsonProperty("safetyMaxValue")]
        public float SafetyMaxValue;

        /// <summary>
        /// Gets or sets  safetyMaxIncrement
        /// </summary>
        [JsonProperty("safetyMaxIncrement")]
        public string SafetyMaxIncrement;

        /// <summary>
        /// Gets or sets  safetyMaxDecrement
        /// </summary>
        [JsonProperty("safetyMaxDecrement")]
        public string SafetyMaxDecrement;

        /// <summary>
        /// Gets or sets  safetyMaxType
        /// </summary>
        [JsonProperty("safetyMaxType")]
        public string SafetyMaxType;

        /// <summary>
        /// Gets or sets  safetyMaxSymbol
        /// </summary>
        [JsonProperty("safetyMaxSymbol")]
        public string SafetyMaxSymbol;
    }
}