using System.Collections.Generic;

namespace CoreDDC.Config
{
    public sealed class CoreDDCConfig
    {
        public AddressLookupSettingsConfig AddressLookupSettings { get; } = new AddressLookupSettingsConfig();
        public IList<PublicIPAddressProviderConfig> AddressProviders { get; } = new List<PublicIPAddressProviderConfig>();
        public IList<UpdateProviderConfig> UpdateProviders { get; } = new List<UpdateProviderConfig>();
    }
}