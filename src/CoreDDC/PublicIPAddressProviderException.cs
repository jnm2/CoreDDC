using System;

namespace CoreDDC
{
    public class PublicIPAddressProviderException : Exception
    {
        public PublicIPAddressProviderException(string message) : base(message)
        {
        }
    }
}
