using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IOutboundHandlerService
    {
        byte[] RequestData { get; }
        Task<Message> ExecuteAsync(Message message, TimeSpan timeout);
        void ProcessResponseData(byte[] data);
    }
}
