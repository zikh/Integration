using System.Threading.Tasks;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IInboundServer
    {
        Task StartServerAsync(IInboundHandlerService inboundHandlerService);
        Task StopServerAsync();
    }
}
