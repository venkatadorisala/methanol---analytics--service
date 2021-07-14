// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Methanol.Services.Config
{
    using JM.Integration.Methanol.Common;

    /// <summary>
    /// ExamScheduleFunctionConfiguration.
    /// </summary>
    public class MethanolFunctionConfiguration : IFunctionConfiguration
    {
        public MethanolFunctionConfiguration()
        {
        }



        ///// <summary>
        ///// Gets or sets ApplicationInsightsInstrumentationKey.
        ///// </summary>
        //public string ApplicationInsightsInstrumentationKey { get; set; }

        /// <summary>
        /// Gets or sets AzureStorageConnectionString.
        /// </summary>
        public string AzureStorageConnectionString { get; set; }
        public string Gen2DataLakeConnectionString { get; set; }



    }
}