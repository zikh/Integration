namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface ILoggingSettingMonitor
    {
        ILoggingSettings LoggingSetting { get; }

        void UpdateLoggingSetting(ILoggingSettings loggingSetting);
    }
}