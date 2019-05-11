namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface ILogger
    {
        void LogInformation(string informaiton);
        void LogTrace(string trace);
        void LogError(string error);
    }
}
