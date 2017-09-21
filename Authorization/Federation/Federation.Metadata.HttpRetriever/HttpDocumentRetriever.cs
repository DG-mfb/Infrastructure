﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Federation.MetadataConsumer;

namespace Federation.Metadata.HttpRetriever
{
    public class HttpDocumentRetriever : IDocumentRetriever
    {
        private Func<HttpClient> _httpClientFactory;

        public bool RequireHttps { get; set; } = true;
        
        public HttpDocumentRetriever(Func<HttpClient> httpClientFactory)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException("httpClientFactory");

            this._httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetDocumentAsync(string address, CancellationToken cancel)
        {
            int num = 0;
            if ((uint)num > 1U)
            {
                if (string.IsNullOrWhiteSpace(address))
                    throw new ArgumentNullException("address");

                if (!Utility.IsHttps(address) && this.RequireHttps)
                    throw new ArgumentException(string.Format("IDX10108: The address specified '{0}' is not valid as per HTTPS scheme. Please specify an https address for security reasons. If you want to test with http address, set the RequireHttps property  on IDocumentRetriever to false.", (object)address), "address");
            }
            string str1;
            try
            {
                var httpClient = this._httpClientFactory();
                if (httpClient == null)
                    throw new ArgumentNullException("httpClient");

                var httpResponseMessage = await httpClient.GetAsync(address, cancel)
                    .ConfigureAwait(false);

                var response = httpResponseMessage;
                httpResponseMessage = null;
                response.EnsureSuccessStatusCode();
                var str = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);
                str1 = str;
            }
            catch (Exception ex)
            {
                throw new IOException(String.Format("IDX10804: Unable to retrieve document from: '{0}'.", address), ex);
            }
            return str1;
        }
    }
}