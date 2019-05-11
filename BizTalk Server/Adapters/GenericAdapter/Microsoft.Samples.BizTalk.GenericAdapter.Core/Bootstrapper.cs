using Unity;
using Unity.Interception;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public static partial class Bootstrapper
    {
        public static readonly IUnityContainer Container;
        static Bootstrapper()
        {
            Container = new UnityContainer();
            Container.AddExtension(new Diagnostic());
            Container.AddNewExtension<Interception>();
        }
    }
}
