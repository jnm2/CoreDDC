using System;

namespace CoreDDC.Config
{
    public sealed class AddressLookupSettingsConfig
    {
        private int minimumAgreeingProviderCount = 3;
        public int MinimumAgreeingProviderCount
        {
            get { return minimumAgreeingProviderCount; }
            set { minimumAgreeingProviderCount = Math.Max(1, value); }
        }

        private double minimumAgreeingProviderPercentage = 90;
        public double MinimumAgreeingProviderPercentage
        {
            get { return minimumAgreeingProviderPercentage; }
            set
            {
                if (value <= 50 || 100 < value) throw new ArgumentOutOfRangeException(nameof(value), value, "Minimum agreement percentage must be greater than 50% and no greater than 100%.");
                minimumAgreeingProviderPercentage = value;
            }
        }

        private int minimumAgreeingEncryptedProviderCount = 2;
        public int MinimumAgreeingEncryptedProviderCount
        {
            get { return minimumAgreeingEncryptedProviderCount; }
            set { minimumAgreeingEncryptedProviderCount = Math.Max(1, value); }
        }

        private double minimumAgreeingEncryptedProviderPercentage = 100;
        public double MinimumAgreeingEncryptedProviderPercentage
        {
            get { return minimumAgreeingEncryptedProviderPercentage; }
            set
            {
                if (value <= 50 || 100 < value) throw new ArgumentOutOfRangeException(nameof(value), value, "Minimum agreement percentage must be greater than 50% and no greater than 100%.");
                minimumAgreeingEncryptedProviderPercentage = value;
            }
        }

        public bool RandomizeProviderOrder { get; set; } = true;
    }
}