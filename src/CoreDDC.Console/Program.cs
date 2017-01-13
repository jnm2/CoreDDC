using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CoreDDC.Config;
using Newtonsoft.Json;

namespace CoreDDC.Console
{
    public sealed class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            var config = LoadConfig();
            //var consensusEngine = IPAddressConsensusEngine.Run(config.AddressLookupSettings, config.AddressProviders);
            ;
            //var updateEngine = new UpdateEngine(LoadConfig());
            //await updateEngine.UpdateAll(await updateEngine.GetCurrentAddress(CancellationToken.None), true, CancellationToken.None);
        }

        private static CoreDDCConfig LoadConfig()
        {
            using (var file = File.OpenText("config.json"))
            using (var reader = new JsonTextReader(file))
                return new JsonSerializer().Deserialize<CoreDDCConfig>(reader);
        }
    }
}
