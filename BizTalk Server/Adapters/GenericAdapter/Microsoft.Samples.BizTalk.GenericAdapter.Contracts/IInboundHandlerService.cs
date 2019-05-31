using Microsoft.ServiceModel.Channels.Common;
using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IInboundHandlerService
    {
        Task StartServerAsync(string[] actions, TimeSpan timeout);
        Task StopServerAsync(TimeSpan timeout);
        Task<Tuple<bool, Message, IInboundReply>> TryReceiveAsync(TimeSpan timeout);
        Task<bool> WaitForMessageAsync(TimeSpan timeout);
        void ProcessRquestData(byte[] data, TaskCompletionSource<Message> responseRawDataAvailableTcs, TaskCompletionSource<object> responseSentTcs);
    }
}
