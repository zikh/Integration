using AsyncNet.Tcp.Client;
using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using ICICI.EAI.Common.Extensions;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Tcp
{
    public class TcpOutboundClient : IOutboundClient
    {
        IAsyncNetTcpClient _tcpClient;
        ApplicationMessageFactory _messageFactory;
        CancellationTokenSource _cts;

        public TcpOutboundClient(IAsyncNetTcpClient tcpClient, ApplicationMessageFactory messageFactory)
        {
            _tcpClient = tcpClient;
            _messageFactory = messageFactory;
            _cts = new CancellationTokenSource();
        }

        public async Task<Message> SendAsync(Message message)
        {
            var tcs = new TaskCompletionSource<Message>();

            _tcpClient.ConnectionEstablished += async (s, e) =>
            {
                await e.RemoteTcpPeer.SendAsync(message.GetReaderAtBodyContents().ReadInnerXml().Map(Encoding.ASCII.GetBytes));
            };

            _tcpClient.FrameArrived += (s, e) =>
            {
                tcs.TrySetResult(_messageFactory.CreateApplicationMessage(e.FrameData).Message);
                e.RemoteTcpPeer.Disconnect(AsyncNet.Tcp.Connection.ConnectionCloseReason.LocalShutdown);
            };

            await _tcpClient.StartAsync(_cts.Token);

            return await tcs.Task;
        }
    }
}
