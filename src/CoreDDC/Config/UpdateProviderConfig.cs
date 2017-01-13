namespace CoreDDC.Config
{
    public sealed class UpdateProviderConfig
    {
        public string ProviderType { get; set; }
        public bool UpdateOnlyIfAddressChanges { get; set; } = true;
        public string UpdateUrl { get; set; }
        public string Host { get; set; }
        public string Domain { get; set; }
        public string Password { get; set; }
    }
}