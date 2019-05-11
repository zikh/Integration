using System.Net;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IInboundServerConfiguration
    {
        IPAddress IPAddress { get; }
        int Port { get; }
    }
}
