﻿using System.Net;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IOutboundClientConfiguration
    {
        IPAddress IPAddress { get; }
        int Port { get; }
    }
}
