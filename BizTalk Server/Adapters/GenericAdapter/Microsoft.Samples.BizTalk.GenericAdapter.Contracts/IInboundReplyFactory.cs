using Microsoft.ServiceModel.Channels.Common;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IInboundReplyFactory
    {
        IInboundReply CreateIInboundReply(TaskCompletionSource<byte[]> responseRawDataAvailableTcs, TaskCompletionSource<object> responseSentTcs);
    }
}
