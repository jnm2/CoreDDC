using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreDDC.Config;

namespace CoreDDC
{
    // To test:
    // * Responses to requests
    // * Timeouts to start backup requests if not all new providers have finished and any more providers exist

    public interface IIPAddressProviderExecutor
    {
        Task<IPAddress> Execute(PublicIPAddressProviderConfig config);
    }

    public interface ITimeoutProvider
    {
        Task Delay(int milliseconds, CancellationToken cancellationToken);
    }
    
    public sealed class IPAddressConsensusEngine
    {

        public static IPAddressConsensusEngine Run(IIPAddressProviderExecutor executor, ITimeoutProvider timeoutProvider, AddressLookupSettingsConfig config, IEnumerable<PublicIPAddressProviderConfig> providers)
            => new IPAddressConsensusEngine(executor, timeoutProvider, config, providers);


        private readonly IIPAddressProviderExecutor executor;
        private readonly ITimeoutProvider timeoutProvider;
        private readonly PublicIPAddressProviderConfig[] providers;
        private int nextProviderIndex = 0;

        private readonly int minimumAgreeingEncryptedProviderCount;
        private readonly double minimumAgreeingEncryptedProviderPercentage;
        private readonly int minimumAgreeingProviderCount;
        private readonly double minimumAgreeingProviderPercentage;
        private readonly Dictionary<IPAddress, ResultState> resultStates = new Dictionary<IPAddress, ResultState>();
        private readonly List<Exception> exceptions = new List<Exception>();

        private sealed class ResultState
        {
            public int Count;
            public int EncryptedCount;
        }

        private IPAddressConsensusEngine(IIPAddressProviderExecutor executor, ITimeoutProvider timeoutProvider, AddressLookupSettingsConfig config, IEnumerable<PublicIPAddressProviderConfig> providers)
        {
            this.executor = executor;
            this.timeoutProvider = timeoutProvider;
            this.providers = GetSortedProviders(providers, config.RandomizeProviderOrder);
            minimumAgreeingEncryptedProviderCount = config.MinimumAgreeingEncryptedProviderCount;
            minimumAgreeingEncryptedProviderPercentage = config.MinimumAgreeingEncryptedProviderPercentage;
            minimumAgreeingProviderCount = config.MinimumAgreeingProviderCount;
            minimumAgreeingProviderPercentage = config.MinimumAgreeingProviderPercentage;

            var numInitialProvidersToQuery = minimumAgreeingProviderCount + 1;
            ExecuteNextProviders(numInitialProvidersToQuery);
        }

        
        private void ExecuteNextProviders(int numProviders)
        {
            if (nextProviderIndex == providers.Length) throw new Exception("Not enough providers to obtain consensus."); // TODO: to completion source
            var provider = providers[nextProviderIndex];
            nextProviderIndex++;
            executor.Execute(provider).ContinueWith(ProviderFinished);
        }

        private void ProviderFinished(Task<IPAddress> task)
        {
            if (task.Exception != null)
            {
                exceptions.Add(task.Exception);
                throw new NotImplementedException();
            }
            else
            {

                throw new NotImplementedException();
            }
        }
        

        private static PublicIPAddressProviderConfig[] GetSortedProviders(IEnumerable<PublicIPAddressProviderConfig> providers, bool shuffle)
        {
            var encryptedProviders = new List<PublicIPAddressProviderConfig>();
            var unencryptedProviders = new List<PublicIPAddressProviderConfig>();

            foreach (var provider in providers)
                (new Uri(provider.Url).Scheme == "https" ? encryptedProviders : unencryptedProviders).Add(provider);

            if (shuffle)
            {
                encryptedProviders.Shuffle();
                unencryptedProviders.Shuffle();
            }

            var r = new PublicIPAddressProviderConfig[encryptedProviders.Count + unencryptedProviders.Count];
            encryptedProviders.CopyTo(r, 0);
            unencryptedProviders.CopyTo(r, encryptedProviders.Count);
            return r;
        }

        private void AfterStateChange()
        {
            var consensus = TryGetCurrentConsensus();
            if (consensus != null) { }

            ;
        }

        private IPAddress TryGetCurrentConsensus()
        {
            if (resultStates.Count == 0) return null;

            throw new NotImplementedException();
        }


        private static T CreateProvider<T>(string typeName) => (T)Activator.CreateInstance(Type.GetType(typeName, true, true));
    }
}
