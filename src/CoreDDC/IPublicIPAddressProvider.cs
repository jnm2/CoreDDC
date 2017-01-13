using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CoreDDC.Config;

namespace CoreDDC
{
    public interface IPublicIPAddressProvider
    {
        Task<IPAddress> Get(PublicIPAddressProviderConfig config, CancellationToken cancellationToken);
        bool IsEncrypted(PublicIPAddressProviderConfig config);
    }
}