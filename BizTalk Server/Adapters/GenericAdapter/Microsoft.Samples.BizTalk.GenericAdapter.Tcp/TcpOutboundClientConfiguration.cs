using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using ICICI.EAI.Common.Utilities;
using System;
using System.Net;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Tcp
{
    public class TcpOutboundClientConfiguration : IOutboundClientConfiguration
    {
        public TcpOutboundClientConfiguration()
        {
            IPAddress = NetworkUtilities.GetLocalIPAddress();
            Port = 9901;
        }

        public IPAddress IPAddress { get; set; }
        public int Port { get; set; }
    }
}
