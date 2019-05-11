using System.ServiceModel.Channels;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IApplicationMessage
    {
        string ProcCode { get; }
        string Backend { get; }
        string Format { get; }
        string Trail { get; }
        Message Message { get; }
        Message ErrorMessage { get; }
        Message TimeoutMessage { get; }
    }
}
