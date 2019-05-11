using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Microsoft.ServiceModel.Channels.Common;
/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterOutboundHandler.cs
/// Description :  This class is used for sending data to the target system
/// -----------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel.Channels;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterOutboundHandler : GenericAdapterHandlerBase, IOutboundHandler
    {
        IOutboundHandlerService _handlerService;
        public GenericAdapterOutboundHandler(IOutboundHandlerService handlerService)
        {
            _handlerService = handlerService;
        }

        public Message Execute(Message message, TimeSpan timeout)
        {
            return _handlerService.ExecuteAsync(message, timeout).Result;
        }
    }
}
