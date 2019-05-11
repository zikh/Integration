namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IApplicationMessageFactory
    {
        IApplicationMessage CreateApplicationMessage(byte[] data);
    }
}
