// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Common
{
    using IdentityModel.Client;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// IHttpService.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// AddTokenHeader.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="additionalHeaders"></param>
        void AddTokenHeader(string token, Dictionary<string, string> additionalHeaders = null);

        /// <summary>
        /// PostAsync.
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(HttpRequestMessage httpRequestMessage);

        /// <summary>
        ///
        /// </summary>
        /// <param name="clientCredentialsTokenRequest"></param>
        /// <returns></returns>
        Task<string> GetApiToken(ClientCredentialsTokenRequest clientCredentialsTokenRequest);

        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <param name="baseurl"></param>
        /// <param name="endpoint"></param>
        /// <param name="clientCredentialsTokenRequest"></param>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(Uri baseurl, string endpoint, string correlationId = "");

        /// <summary>
        /// PostAsync
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent httpContent);

        /// <summary>
        /// GetBaseAddress.
        /// </summary>
        /// <returns></returns>
        Uri GetBaseAddress();
    }
}