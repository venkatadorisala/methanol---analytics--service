// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.
namespace JM.Integration.Methanol.Services
{
    using IdentityModel.Client;
    using JM.Integration.Common;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// ReferenceDataHttpService.
    /// </summary>
    public class HttpService : IHttpService
    {
        private HttpClient httpClient;
        private HttpClient httpClienttoken;

        public HttpService(IHttpClientFactory httpClientFactory = null)
        {
            this.httpClient = httpClientFactory.CreateClient("HttpPostService");
            this.httpClienttoken = httpClientFactory.CreateClient("HttpTokenService");
        }

        public void AddTokenHeader(string token, Dictionary<string, string> additionalHeaders = null)
        {
            this.httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrWhiteSpace(token))
            {
                this.httpClient.DefaultRequestHeaders.Add("Authorization", token);
            }

            if (additionalHeaders != null)
            {
                foreach (KeyValuePair<string, string> x in additionalHeaders)
                {
                    this.httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                }
            }
        }

        /// <summary>
        /// GetBaseAddress.
        /// </summary>
        /// <returns></returns>
        public Uri GetBaseAddress()
        {
            return this.httpClient.BaseAddress;
        }

        /// <summary>
        /// PostAsync.
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(HttpRequestMessage httpRequestMessage)
        {
            return await this.httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Method used for POST call.
        /// </summary>
        /// <param name="uri">uri.</param>
        /// <param name="httpContent">httpContent.</param>
        /// <returns>HttpResponseMessage.</returns>
        public async Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent httpContent)
        {
            return await this.httpClient.PostAsync(uri, httpContent).ConfigureAwait(false);
        }

        /// <summary>
        /// Method used for Get call.
        /// </summary>
        /// <param name="baseurl">baseurl.</param>
        /// <param name="endpoint">url.</param>
        /// <param name="correlationId">correlationId.</param>
        /// <param name="correlationId">correlationId.</param>
        /// <returns>HttpResponseMessage.</returns>
        public async Task<HttpResponseMessage> GetAsync(Uri baseurl, string endpoint, string correlationId = "")
        {
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            this.httpClient.BaseAddress = baseurl;

            this.httpClient.DefaultRequestHeaders.Add("Co-RelationId", correlationId);

            var httpResponse = await this.httpClient.GetAsync(endpoint).ConfigureAwait(false);
            return httpResponse;
        }

        /// <summary>
        /// To get existing API Token from Cache
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetApiToken(ClientCredentialsTokenRequest clientCredentialsTokenRequest)
        {
            TokenResponse tokenAPIResponse = await HttpClientTokenRequestExtensions.RequestClientCredentialsTokenAsync(httpClienttoken, clientCredentialsTokenRequest).ConfigureAwait(false);

            return tokenAPIResponse.AccessToken;
        }
    }
}