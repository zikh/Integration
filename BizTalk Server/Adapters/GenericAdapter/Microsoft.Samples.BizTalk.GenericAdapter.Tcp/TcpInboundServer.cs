using AsyncNet.Tcp.Server;
using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using ICICI.EAI.Common.Extensions;
using Microsoft.ServiceModel.Channels.Common;
using System;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Tcp
{
    public class TcpInboundServer : IInboundServer
    {
        IAsyncNetTcpServer _tcpServer;
        IInboundQueue<Tuple<bool, Message, IInboundReply>> _tcpQueue;
        IApplicationMessageFactory _messageFactory;
        IInboundReplyFactory _replyFactory;
        CancellationTokenSource _cts;

        public TcpInboundServer(IAsyncNetTcpServer tcpServer, IInboundQueue<Tuple<bool, Message, IInboundReply>> tcpQueue, ApplicationMessageFactory messageFactory, IInboundReplyFactory replyFactory)
        {
            _tcpServer = tcpServer;
            _tcpQueue = tcpQueue;
            _messageFactory = messageFactory;
            _replyFactory = replyFactory;

            _cts = new CancellationTokenSource();
        }

        public async Task StartServerAsync()
        {
            _tcpServer.FrameArrived += async (s, e) =>
            {
                var applicationMessage = _messageFactory.CreateApplicationMessage(e.FrameData);

                var reply = _replyFactory.CreateIInboundReply(new TaskCompletionSource<Message>(), new TaskCompletionSource<object>()) as ApplicationReply;

                _tcpQueue.Enqueue(new Tuple<bool, Message, IInboundReply>(true, applicationMessage.Message, reply));

                var responseMessage = await reply.ResponseAvailableTaskCompletionSource.Task;

                await e.RemoteTcpPeer.SendAsync(responseMessage.GetReaderAtBodyContents().ReadInnerXml().Map(Encoding.ASCII.GetBytes));

                reply.ResponseSentTaskCompletionSource.TrySetResult(null);

                e.RemoteTcpPeer.Disconnect(AsyncNet.Tcp.Connection.ConnectionCloseReason.LocalShutdown);
            };

            await _tcpServer.StartAsync(_cts.Token);
        }

        public async Task StopServerAsync()
        {
            foreach (var connectedPeer in _tcpServer.ConnectedPeers)
            {
                connectedPeer.Disconnect(AsyncNet.Tcp.Connection.ConnectionCloseReason.LocalShutdown);
            }
            _cts.Cancel();

            await Task.FromResult(_cts.Token);
        }
    }
}
