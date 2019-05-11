namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IAdministrationServices
    {
        void ConfigureRedis();
        void ConfigureLogging();
    }
}