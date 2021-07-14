// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Methanol.Common
{
    public interface IFunctionConfiguration
    {
        ///// <summary>
        ///// Gets or sets ApplicationInsightsInstrumentationKey.
        ///// </summary>
        // string ApplicationInsightsInstrumentationKey { get; set; }

        /// <summary>
        /// Gets or sets AzureStorageConnectionString.
        /// </summary>
        string AzureStorageConnectionString { get; set; }
    }
}