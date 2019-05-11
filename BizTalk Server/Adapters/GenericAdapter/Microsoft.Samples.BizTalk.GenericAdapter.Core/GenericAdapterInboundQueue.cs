using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Microsoft.Samples.BizTalk.GenericAdapter.Utilities;
using Microsoft.ServiceModel.Channels.Common;
using System;
using System.ServiceModel.Channels;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterInboundQueue : IInboundQueue<Tuple<bool, Message, IInboundReply>>
    {
        BlockingQueue<Tuple<bool, Message, IInboundReply>> _queue;
        public GenericAdapterInboundQueue()
        {
            _queue = new BlockingQueue<Tuple<bool, Message, IInboundReply>>();
        }
        public Tuple<bool, Message, IInboundReply> Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(Tuple<bool, Message, IInboundReply> message)
        {
            _queue.Enqueue(message);
        }
    }
}
