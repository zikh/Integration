/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterInboundHandler.cs
/// Description :  This class implements an interface for listening or polling for data.
/// -----------------------------------------------------------------------------------------------------------
using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Microsoft.ServiceModel.Channels.Common;
using System;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterInboundHandler : GenericAdapterHandlerBase, IInboundHandler
    {
        IInboundHandlerService _handlerService;

        /// <summary>
        /// Initializes a new instance of the CustomAdapterInboundHandler class
        /// </summary>
        public GenericAdapterInboundHandler(IInboundHandlerService handlerService)
        {
            _handlerService = handlerService;
        }

        /// <summary>
        /// Start the listener
        /// </summary>
        public void StartListener(string[] actions, TimeSpan timeout)
        {
            _handlerService.StartServerAsync(actions, timeout);
        }

        /// <summary>
        /// Stop the listener
        /// </summary>
        public void StopListener(TimeSpan timeout)
        {
            _handlerService.StopServerAsync(timeout);
        }

        /// <summary>
        /// Tries to receive a message within a specified interval of time. 
        /// </summary>
        public bool TryReceive(TimeSpan timeout, out System.ServiceModel.Channels.Message message, out IInboundReply reply)
        {
            var tuple = _handlerService.TryReceiveAsync(timeout).Result;

            message = tuple.Item2;
            reply = tuple.Item3;

            return tuple.Item1;
        }

        /// <summary>
        /// Returns a value that indicates whether a message has arrived within a specified interval of time.
        /// </summary>
        public bool WaitForMessage(TimeSpan timeout)
        {
            return _handlerService.WaitForMessageAsync(timeout).Result;
        }
    }
}
