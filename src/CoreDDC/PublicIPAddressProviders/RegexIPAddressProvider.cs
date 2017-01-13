using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreDDC.Config;

namespace CoreDDC.PublicIPAddressProviders
{
    public sealed class RegexIPAddressProvider : HttpGetIPAddressProvider
    {
        protected override async Task<IPAddress> ExtractAddress(PublicIPAddressProviderConfig config, HttpContent responseBody)
        {
            var match = Regex.Match(await responseBody.ReadAsStringAsync(), config.Pattern);
            if (!match.Success) throw new PublicIPAddressProviderException("Failed to match the regex pattern.");
            return IPAddress.Parse(match.Value);
        }
    }
}
