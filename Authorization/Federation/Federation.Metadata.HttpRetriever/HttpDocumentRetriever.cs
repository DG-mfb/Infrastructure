using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Federation.MetadataConsumer;

namespace Federation.Metadata.HttpRetriever
{
    public class HttpDocumentRetriever : IDocumentRetriever
    {
        private static readonly HttpClient _defaultHttpClient = new HttpClient();
        private HttpClient _httpClient;

        public bool RequireHttps { get; set; } = true;

        public HttpDocumentRetriever()
        {
        }

        public HttpDocumentRetriever(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");
            this._httpClient = httpClient;
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
                //IdentityModelEventSource.Logger.WriteVerbose("IDX10805: Obtaining information from metadata endpoint: '{0}'.", (object)address);
                HttpClient httpClient = this._httpClient ?? HttpDocumentRetriever._defaultHttpClient;
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(address, cancel).ConfigureAwait(false);
                HttpResponseMessage response = httpResponseMessage;
                httpResponseMessage = (HttpResponseMessage)null;
                response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                str1 = str;
            }
            catch (Exception ex)
            {
                throw new IOException(string.Format("IDX10804: Unable to retrieve document from: '{0}'.", (object)address), ex);
            }
            return str1;
        }

        private static class Utility
        {
            public static bool IsHttps(string address)
            {
                if (string.IsNullOrEmpty(address))
                    return false;
                try
                {
                    Uri uri = new Uri(address);
                    return Utility.IsHttps(new Uri(address));
                }
                catch (UriFormatException)
                {
                    return false;
                }
            }

            public static bool IsHttps(Uri uri)
            {
                if (uri == (Uri)null)
                    return false;
                return uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}