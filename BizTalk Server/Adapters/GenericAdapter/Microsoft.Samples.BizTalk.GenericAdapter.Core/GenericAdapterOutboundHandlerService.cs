/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterOutboundHandler.cs
/// Description :  This class is used for sending data to the target system
/// -----------------------------------------------------------------------------------------------------------

using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Polly.CircuitBreaker;
using Polly.Registry;
using System;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterOutboundHandlerService : IOutboundHandlerService
    {
        IOutboundClient _outboundClient;
        IApplicationMessageFactory _messageFactory;
        IPolicyRegistry<string> _policyRegistry;

        Message _message;

        TaskCompletionSource<Message> _tcs;

        public byte[] RequestData { get { return Encoding.ASCII.GetBytes(_message.GetReaderAtBodyContents().ReadInnerXml()); } }

        public GenericAdapterOutboundHandlerService(IOutboundClient outboundClient, IApplicationMessageFactory messageFactory, IPolicyRegistry<string> policyRegistry)
        {
            _tcs = new TaskCompletionSource<Message>();
            _outboundClient = outboundClient;
            _messageFactory = messageFactory;
            _policyRegistry = policyRegistry;
        }

        public async Task<Message> ExecuteAsync(Message message, TimeSpan timeout)
        {
            var buffer = message.CreateBufferedCopy(Int16.MaxValue);
            _message = buffer.CreateMessage();
            buffer.Close();

            await _outboundClient.StartClientAsync(this);

            return await _tcs.Task;
        }

        public void ProcessResponseData(byte[] data)
        {
            var applicationMessage = _messageFactory.CreateApplicationMessage(data);

            var circuitBreaker = _policyRegistry.Get<CircuitBreakerPolicy<Message>>(applicationMessage.Backend);

            circuitBreaker.Execute(() => applicationMessage.Message);

            _tcs.TrySetResult(applicationMessage.Message);
        }
    }
}
