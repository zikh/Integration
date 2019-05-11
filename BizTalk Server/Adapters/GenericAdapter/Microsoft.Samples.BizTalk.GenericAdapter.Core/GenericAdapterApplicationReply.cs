using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using System;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterApplicationReply : ApplicationReply
    {
        CancellationTokenSource _cts;

        public GenericAdapterApplicationReply()
        {
            _cts = new CancellationTokenSource();
        }

        public override void Abort()
        {
            _cts.Cancel();
        }

        public override void Reply(Message message, TimeSpan timeout)
        {
            var buffer = message.CreateBufferedCopy(Int16.MaxValue);
            ReplyAsync(buffer, timeout).Wait();
            buffer.Close();
        }

        private async Task ReplyAsync(MessageBuffer buffer, TimeSpan timeout)
        {
            var message = buffer.CreateMessage();

            ResponseAvailableTaskCompletionSource.TrySetResult(Encoding.ASCII.GetBytes(message.GetReaderAtBodyContents().ReadInnerXml()));
            await ResponseSentTaskCompletionSource.Task;
        }
    }
}
