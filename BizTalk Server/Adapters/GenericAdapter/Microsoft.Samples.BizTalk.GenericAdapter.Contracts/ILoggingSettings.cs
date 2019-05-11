namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface ILoggingSettings
    {
        bool TraceEnabled { get; set; }
        bool InformationEnabled { get; set; }
        bool ErrorEnabled { get; set; }
    }
}
