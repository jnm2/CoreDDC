using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using CoreDDC.Config;

namespace CoreDDC.DynamicDnsProviders
{
    public sealed class NamecheapProvider : IDynamicDnsProvider
    {
        private const string DefaultUrl = "https://dynamicdns.park-your-domain.com/update";

        public async Task Update(UpdateProviderConfig providerConfig, IPAddress newAddress, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(providerConfig.UpdateUrl ?? DefaultUrl);
            if (builder.Scheme.ToLower() != "https") throw new InvalidOperationException("The update URL must be HTTPS so that the password is not sent in the clear.");

            builder.Query += builder.Query + (string.IsNullOrEmpty(builder.Query) ? null : "&")
                + "by=nc&host=" + Uri.EscapeDataString(providerConfig.Host ?? string.Empty)
                + "&domain=" + Uri.EscapeUriString(providerConfig.Domain ?? string.Empty)
                + "&password=" + Uri.EscapeDataString(providerConfig.Password ?? string.Empty)
                + "&ip=" + Uri.EscapeDataString(newAddress.ToString());

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(builder.Uri, HttpCompletionOption.ResponseContentRead, cancellationToken);
                response.EnsureSuccessStatusCode();
                var xmlResponse = XDocument.Parse(await response.Content.ReadAsStringAsync()).Element("interface-response");

                var errorContainer = xmlResponse.Element("errors");
                if (errorContainer != null)
                {
                    var errors = errorContainer.Elements().Select(_ => _.Value).ToList();
                    if (errors.Count != 0)
                        throw new DynamicDnsProviderException(errors.Count == 1
                            ? "Update failed: " + errors.Single()
                            : "Update failed:\r\n" + string.Join("\r\n", errors.Select(error => " * " + error)));
                }
                
                var actualAddress = IPAddress.Parse(xmlResponse.Element("IP").Value);
                if (!newAddress.Equals(actualAddress))
                    throw new DynamicDnsProviderException($"Provider set the address to {actualAddress} instead of {newAddress}.");
            }
        }
    }
}
