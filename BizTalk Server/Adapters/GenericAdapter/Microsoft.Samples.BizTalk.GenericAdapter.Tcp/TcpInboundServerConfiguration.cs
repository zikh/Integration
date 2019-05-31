using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using ICICI.EAI.Common.Utilities;
using System.Net;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Tcp
{
    public class TcpInboundServerConfiguration : IInboundServerConfiguration
    {
        public TcpInboundServerConfiguration()
        {
            IPAddress = NetworkUtilities.GetLocalIPAddress();
            Port = 8801;
        }

        public IPAddress IPAddress { get; set; }

        public int Port { get; set; }
    }
}
