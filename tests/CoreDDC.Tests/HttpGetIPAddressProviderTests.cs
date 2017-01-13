using System.Threading;
using System.Threading.Tasks;
using CoreDDC.Config;
using CoreDDC.PublicIPAddressProviders;
using NUnit.Framework;

namespace CoreDDC.Tests
{
    [TestFixture]
    public class HttpGetIPAddressProviderTests
    {
        [TestCase("https://icanhazip.com/")]
        [TestCase("http://ifconfig.me/ip")]
        [TestCase("http://ipecho.net/plain")]
        [TestCase("https://wtfismyip.com/text")]
        [TestCase("http://checkip.amazonaws.com/")]
        [TestCase("https://ipinfo.io/ip")]
        [TestCase("https://api.ipify.org/")]
        [TestCase("https://myexternalip.com/raw")]
        [TestCase("https://ip.appspot.com/")]
        [TestCase("http://whatismyip.akamai.com/")]
        [TestCase("https://4.ifcfg.me/ip")]
        [TestCase("http://curlmyip.com/")]
        [TestCase("http://ident.me/")]
        [TestCase("http://tnx.nl/ip")]
        [TestCase("https://l2.io/ip")]
        [TestCase("https://ipof.in/txt")]
        [TestCase("https://ip.tyk.nu/")]
        [TestCase("http://eth0.me/")]
        [TestCase("http://wgetip.com/")]
        [TestCase("http://bot.whatismyipaddress.com/")]
        [TestCase("http://ip.dnsexit.com")]
        public Task GetIPAddress(string url)
        {
            return new HttpGetIPAddressProvider().Get(new PublicIPAddressProviderConfig { Url = url }, CancellationToken.None);
        }
    }
}
