﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Configuration;
using Kernel.Security.Validation;
using Kernel.Web;

namespace CircuitBreakerTests.MockData
{
    internal class HttpDocumentRetrieverMock : IDocumentRetriever
    {
        static HttpDocumentRetrieverMock()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private readonly IBackchannelCertificateValidator _backchannelCertificateValidator;

        public bool RequireHttps { get; set; }
        public TimeSpan Timeout { get; set; }
        public long MaxResponseContentBufferSize { get; set; }
        public ICustomConfigurator<HttpDocumentRetrieverMock> HttpDocumentRetrieverConfigurator { private get; set; }

        /// <summary>
        /// Initialise an instance of Http document retriever
        /// </summary>
        /// <param name="backchannelCertificateValidator"></param>
        public HttpDocumentRetrieverMock(IBackchannelCertificateValidator backchannelCertificateValidator)
        {
            if (backchannelCertificateValidator == null)
                throw new ArgumentNullException("backchannelCertificateValidator");

            this._backchannelCertificateValidator = backchannelCertificateValidator;
            this.Timeout = TimeSpan.FromSeconds(30);
            this.MaxResponseContentBufferSize = 10485760L;
            this.RequireHttps = true;
        }

        /// <summary>
        /// Retrieve a detadata document from the web
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<string> GetDocumentAsync(string address, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException("address");
            
            string str1;
            try
            {
                if (this.HttpDocumentRetrieverConfigurator != null)
                {
                    this.HttpDocumentRetrieverConfigurator.Configure(this);
                }

                var messageHandler = new WebRequestHandler
                {
                    ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this._backchannelCertificateValidator.Validate)
                };
                using (messageHandler)
                {
                    var httpClient = new HttpClient(messageHandler)
                    {
                        Timeout = this.Timeout,
                        MaxResponseContentBufferSize = this.MaxResponseContentBufferSize
                    };

                    using (httpClient)
                    {
                        var httpResponseMessage = await httpClient.GetAsync(address, cancel)
                            .ConfigureAwait(true);

                        var response = httpResponseMessage;
                        httpResponseMessage = null;
                        response.EnsureSuccessStatusCode();
                        var str = await response.Content.ReadAsStringAsync()
                            .ConfigureAwait(true);
                        str1 = str;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IOException(String.Format("IDX10804: Unable to retrieve document from: '{0}'.", address), ex);
            }
            return str1;
        }
    }
}
