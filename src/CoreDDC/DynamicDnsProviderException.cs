using System;

namespace CoreDDC
{
    public class DynamicDnsProviderException : Exception
    {
        public DynamicDnsProviderException(string message) : base(message)
        {
        }
    }
}
