using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IOutboundClient
    {
        Task StartClientAsync(IOutboundHandlerService outboundHandlerService);
    }
}
