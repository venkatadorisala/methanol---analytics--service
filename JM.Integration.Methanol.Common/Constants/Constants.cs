// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Methanol.Common.Constants
{
    public static class Constants
    {
        /// <summary>
        /// Constant for ServiceBusConnectionString.
        /// </summary>
        public const string ServiceBusConnectionString = "ServiceBusConnectionString";

        /// <summary>
        /// KeyVaultUrl.
        /// </summary>
        public const string KeyVaultUrl = "KeyVaultUrl";

        /// <summary>
        /// GrantType.
        /// </summary>
        public const string GrantType = "grant_type";

        /// <summary>
        /// RMClientID.
        /// </summary>
        public const string ClientID = "client_id";

        /// <summary>
        /// RMClientSecret.
        /// </summary>
        public const string ClientSecret = "client_secret";

        /// <summary>
        /// HttpPostService.
        /// </summary>
        public const string HttpPostService = "HttpPostService";

        /// <summary>
        /// HttpTokenService.
        /// </summary>
        public const string HttpTokenService = "HttpTokenService";

        /// <summary>
        /// CreateDate.
        /// </summary>
        public const string CreateDate = "CreateDate";

        /// <summary>
        /// DateTimeFormat.
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-ddT00:00:00.0000000Z";

        /// <summary>
        /// Authorization.
        /// </summary>
        public const string Authorization = "Authorization";

        /// <summary>
        /// RMScope.
        /// </summary>
        public const string Scope = "Scope";

        /// <summary>
        /// Processor.
        /// </summary>
        public const string Processor = "IBeADS";

        /// <summary>
        /// Cache Expiration
        /// </summary>
        public const int CacheExpirationinDays = 1;

        /// <summary>
        /// RMAccessTokenPath
        /// </summary>
        public const string AccessTokenPath = "TokenPath";

        /// <summary>
        /// KeyConfig
        /// </summary>
        public const string KeyConfig = "KeyConfig";

        /// <summary>
        /// SchemaFolderName
        /// </summary>
        public const string SchemaFolderName = "SchemaFolderName";

        /// <summary>
        /// Local settings json
        /// </summary>
        public const string LocalSettingsJson = "local.settings.json";

        /// <summary>
        /// Retry
        /// </summary>
        public const int retry = 3;

        /// <summary>
        /// Retry Attempt Power
        /// </summary>
        public const int retryattemptpower = 2;

        /// <summary>
        /// Double Factor
        /// </summary>
        public const double doublefactor = 0.1;

        ///// <summary>
        ///// ApplicationJson.
        ///// </summary>
        public const string ApplicationJson = "application/json";

        /// <summary>
        /// TokenType.
        /// </summary>
        public const string TokenType = "Bearer ";

        /// <summary>
        /// BlobUri.
        /// </summary>
        public const string BlobUri = "Blob";

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public const string ErrorMessage = "Failed to execute {0} for {1}: {2}";
    }
}