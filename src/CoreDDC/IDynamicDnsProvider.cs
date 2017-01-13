using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CoreDDC.Config;

namespace CoreDDC
{
    public interface IDynamicDnsProvider
    {
        Task Update(UpdateProviderConfig info, IPAddress newAddress, CancellationToken cancellationToken);
    }
}
