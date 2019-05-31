using Microsoft.Practices.Unity.Configuration;
using Unity;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public static partial class Bootstrapper
    {
        public static readonly IUnityContainer Container;
        static Bootstrapper()
        {
            Container = new UnityContainer();
            Container.LoadConfiguration();
        }
    }
}
