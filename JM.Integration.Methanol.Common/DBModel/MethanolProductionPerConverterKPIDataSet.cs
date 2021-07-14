using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JM.Integration.Methanol.Common.DBModel
{
    /// <summary>
    /// MethanolProductionPerConverterKPIDataSet
    /// </summary>
    public class MethanolProductionPerConverterKPIDataSet
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
        /// Gets or sets percentLabel
        /// </summary>
        [JsonProperty("percentLabel")]
        public string PercentLabel;

        /// <summary>
        /// Gets or sets percentValue
        /// </summary>
        [JsonProperty("percentValue")]
        public string PercentValue;

        /// <summary>
        /// Gets or sets percentIncrement
        /// </summary>
        [JsonProperty("percentIncrement")]
        public float PercentIncrement;

        /// <summary>
        /// Gets or sets percentDecrement
        /// </summary>
        [JsonProperty("percentDecrement")]
        public float PercentDecrement;

        /// <summary>
        /// Gets or sets percentType
        /// </summary>
        [JsonProperty("percentType")]
        public string PercentType;

        /// <summary>
        /// Gets or sets percentSymbol
        /// </summary>
        [JsonProperty("percentSymbol")]
        public string PercentSymbol;

        /// <summary>
        /// Gets or sets percentId
        /// </summary>
        [JsonProperty("percentId")]
        public string PercentId;

        /// <summary>
        /// Gets or sets actualLabel
        /// </summary>
        [JsonProperty("actualLabel")]
        public string ActualLabel;

        /// <summary>
        /// Gets or sets actualValue
        /// </summary>
        [JsonProperty("actualValue")]
        public string ActualValue;

        /// <summary>
        /// Gets or sets actualIncrement
        /// </summary>
        [JsonProperty("actualIncrement")]
        public float ActualIncrement;

        /// <summary>
        /// Gets or sets actualDecrement
        /// </summary>
        [JsonProperty("actualDecrement")]
        public float ActualDecrement;

        /// <summary>
        /// Gets or sets actualType
        /// </summary>
        [JsonProperty("actualType")]
        public string ActualType;
        //ActualMtpd
        /// <summary>
        /// Gets or sets actualSymbol
        /// </summary>
        [JsonProperty("actualSymbol")]
        public string ActualSymbol;

        /// <summary>
        /// Gets or sets actualId
        /// </summary>
        [JsonProperty("actualId")]
        public string ActualId;

        [JsonProperty("kpiTitle")]
        public string KpiTitle;

        /// <summary>
        /// Gets or sets  maxValue
        /// </summary>
        [JsonProperty("maxValue")]
        public float MaxValue;

        /// <summary>
        /// Gets or sets  maxValue
        /// </summary>
        [JsonProperty("mainValue")]
        public float MainValue;

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
