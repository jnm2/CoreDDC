using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CoreDDC.Config;

namespace CoreDDC
{
    public sealed class UpdateEngine
    {
        private readonly CoreDDCConfig config;
        

        public UpdateEngine(CoreDDCConfig config)
        {
            this.config = config;
        }

        public async Task<IPAddress> GetCurrentAddress(CancellationToken cancellationToken)
        {/*
            var providers = config.AddressProviders.ToList();

            var results = new List<Task<IPAddress>>();
            for (var i = 0; i < Math.Min(config.AddressLookupSettings.MinimumAgreeingProviderCount, providers.Count); i++)
                results.Add(CreateProvider<IPublicIPAddressProvider>(providers[i].ProviderType).Get(providers[i], cancellationToken));

            await Task.WhenAll(results.Take(config.AddressLookupSettings.MinimumAgreeingProviderCount));
            */
            throw new NotImplementedException();
        }

        private static T CreateProvider<T>(string typeName) => (T)Activator.CreateInstance(Type.GetType(typeName, true, true));

        public Task UpdateAll(IPAddress newAddress, bool didChange, CancellationToken cancellationToken)
            => Task.WhenAll(
                from provider in config.UpdateProviders
                where didChange || provider.UpdateOnlyIfAddressChanges
                select CreateProvider<IDynamicDnsProvider>(provider.ProviderType)
                    .Update(provider, newAddress, cancellationToken));
    }
}
