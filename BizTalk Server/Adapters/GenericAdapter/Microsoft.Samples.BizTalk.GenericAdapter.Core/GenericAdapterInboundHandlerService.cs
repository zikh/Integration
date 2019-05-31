using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Microsoft.ServiceModel.Channels.Common;
using Polly;
using Polly.CircuitBreaker;
using Polly.Registry;
using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterInboundHandlerService : IInboundHandlerService
    {
        IInboundServer _inboundServer;
        IInboundQueue<Tuple<bool, Message, IInboundReply>> _inboundQueue;
        IApplicationMessageFactory _messageFactory;
        IInboundReplyFactory _replyFactory;
        IPolicyRegistry<string> _policyRegistry;

        public GenericAdapterInboundHandlerService(IInboundServer inboundServer, IApplicationMessageFactory messageFactory, IInboundQueue<Tuple<bool, Message, IInboundReply>> inboundQueue, IInboundReplyFactory replyFactory, IPolicyRegistry<string> policyRegistry)
        {
            _inboundServer = inboundServer;
            _inboundQueue = inboundQueue;
            _messageFactory = messageFactory;
            _replyFactory = replyFactory;
            _policyRegistry = policyRegistry;
        }

        public async Task StartServerAsync(string[] actions, TimeSpan timeout)
        {
            var startServerTask = _inboundServer.StartServerAsync(this);
            var timeoutTask = Task.Delay(timeout);

            var task = await Task.WhenAny(startServerTask, timeoutTask);

            if (task == timeoutTask)
            {
                // Cancel startServerTask.
                throw new TimeoutException("StartListener timedout.");
            }
        }

        public async Task StopServerAsync(TimeSpan timeout)
        {
            var stopServerTask = _inboundServer.StopServerAsync();
            var timeoutTask = Task.Delay(timeout);

            var task = await Task.WhenAny(stopServerTask, timeoutTask);

            if (task == timeoutTask)
            {
                // Cancel startServerTask.
                throw new TimeoutException("StopServer timedout.");
            }
        }

        public async Task<Tuple<bool, Message, IInboundReply>> TryReceiveAsync(TimeSpan timeout)
        {
            var timeoutTask = Task.Delay(timeout);
            var messageTask = Task.FromResult(_inboundQueue.Dequeue());

            var completedTask = await Task.WhenAny(messageTask, timeoutTask);

            if (completedTask == timeoutTask)
                return new Tuple<bool, Message, IInboundReply>(false, null, null);

            return await messageTask;
        }

        public async Task<bool> WaitForMessageAsync(TimeSpan timeout)
        {
            return await Task.FromResult(false);
        }

        public void ProcessRquestData(byte[] data, TaskCompletionSource<Message> responseRawDataAvailableTcs, TaskCompletionSource<object> responseSentTcs)
        {
            var applicationMessage = _messageFactory.CreateApplicationMessage(data);
            IInboundReply reply = null;

            var messageAvailable = applicationMessage.Message != null;

            reply = _replyFactory.CreateIInboundReply(responseRawDataAvailableTcs, responseSentTcs);

            var circuitBreaker = _policyRegistry.Get<CircuitBreakerPolicy<Message>>(applicationMessage.Backend);

            var policy = Policy.HandleResult<Message>(r => r.IsFault).
                Fallback(applicationMessage.Message, (x) =>
                    {
                        reply.Reply(applicationMessage.ErrorMessage, TimeSpan.FromSeconds(1));
                    }).Wrap(circuitBreaker);

            policy.Execute(() =>
            {
                if (messageAvailable)
                    _inboundQueue.Enqueue(new Tuple<bool, Message, IInboundReply>(messageAvailable, applicationMessage.Message, reply));

                return applicationMessage.Message;
            });
        }
    }
}
