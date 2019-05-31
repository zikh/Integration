using Microsoft.ServiceModel.Channels.Common;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IInboundReplyFactory
    {
        IInboundReply CreateIInboundReply(TaskCompletionSource<Message> responseRawDataAvailableTcs, TaskCompletionSource<object> responseSentTcs);
    }
}
