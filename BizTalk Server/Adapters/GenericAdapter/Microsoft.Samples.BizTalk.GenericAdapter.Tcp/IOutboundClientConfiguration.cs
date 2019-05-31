using System.Net;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Tcp
{
    public interface IOutboundClientConfiguration
    {
        IPAddress IPAddress { get; }
        int Port { get; }
    }
}
