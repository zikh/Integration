using System.Net;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Tcp
{
    public interface IInboundServerConfiguration
    {
        IPAddress IPAddress { get; }
        int Port { get; }
    }
}
