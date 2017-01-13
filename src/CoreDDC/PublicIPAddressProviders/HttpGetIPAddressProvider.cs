using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoreDDC.Config;

namespace CoreDDC.PublicIPAddressProviders
{
    public class HttpGetIPAddressProvider : IPublicIPAddressProvider
    {
        private readonly HttpClient client = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = true,
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
        });

        public async Task<IPAddress> Get(PublicIPAddressProviderConfig config, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(new Uri(config.Url), HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                return await ExtractAddress(config, response.Content);
            }
        }

        public bool IsEncrypted(PublicIPAddressProviderConfig config)
        {
            return "https".Equals(new Uri(config.Url).Scheme, StringComparison.OrdinalIgnoreCase);
        }

        protected virtual async Task<IPAddress> ExtractAddress(PublicIPAddressProviderConfig config, HttpContent responseBody)
        {
            return IPAddress.Parse((await responseBody.ReadAsStringAsync()).Trim());
        }
    }
}
