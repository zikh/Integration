using Microsoft.ServiceModel.Channels.Common;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public abstract class ApplicationReply : InboundReply
    {
        public TaskCompletionSource<byte[]> ResponseAvailableTaskCompletionSource { get; set; }
        public TaskCompletionSource<object> ResponseSentTaskCompletionSource { get; set; }
    }
}
