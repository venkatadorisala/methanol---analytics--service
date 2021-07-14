// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Common.Middleware
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Api Response Class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class ApiResponse
    {
        /// <summary>
        /// Gets or sets Result.
        /// </summary>
        /// <value>
        /// Result.
        /// </value>
        [JsonProperty("result")]
        public object Result { get; set; }

        /// <summary>
        /// Gets or Sets ServiceName.
        /// </summary>
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or Sets StatusCode.
        /// </summary>
        [JsonProperty("statusCode")]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets StatusDescription.
        /// </summary>
        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or Sets Errors.
        /// </summary>
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        /// <summary>
        /// Returns the string presentation of the object.
        /// </summary>
        /// <returns>String presentation of the object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApiResponse {\n");
            sb.Append("  ServiceName: ").Append(this.ServiceName).Append("\n");
            sb.Append("  StatusCode: ").Append(this.StatusCode).Append("\n");
            sb.Append("  StatusDescription: ").Append(this.StatusDescription).Append("\n");
            sb.Append("  Errors: ").Append(this.Errors).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object.
        /// </summary>
        /// <returns>JSON string presentation of the object.</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}