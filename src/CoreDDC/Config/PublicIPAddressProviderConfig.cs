namespace CoreDDC.Config
{
    public sealed class PublicIPAddressProviderConfig
    {
        public string ProviderType { get; set; }
        public string Url { get; set; }
        public string Pattern { get; set; }
    }
}