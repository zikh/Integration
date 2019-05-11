using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Unity;
using Unity.Resolution;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterApplicationMessageFactory : IApplicationMessageFactory
    {
        IUnityContainer _container;
        public GenericAdapterApplicationMessageFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IApplicationMessage CreateApplicationMessage(byte[] data)
        {
            var messageType = ApplicationMessageType(data);
            return _container.Resolve<IApplicationMessage>(messageType, new ParameterOverride("data", data));
        }

        string ApplicationMessageType(byte[] data)
        {
            return "XML";
        }
    }
}
