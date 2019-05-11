using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Microsoft.ServiceModel.Channels.Common;
using System.Threading.Tasks;
using Unity;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterInboundReplyFactory : IInboundReplyFactory
    {
        IUnityContainer _container;

        public GenericAdapterInboundReplyFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IInboundReply CreateIInboundReply(TaskCompletionSource<byte[]> responseRawDataAvailableTcs, TaskCompletionSource<object> responseSentTcs)
        {
            var reply = _container.Resolve<ApplicationReply>();
            reply.ResponseAvailableTaskCompletionSource = responseRawDataAvailableTcs;
            reply.ResponseSentTaskCompletionSource = responseSentTcs;

            return reply;
        }
    }
}
