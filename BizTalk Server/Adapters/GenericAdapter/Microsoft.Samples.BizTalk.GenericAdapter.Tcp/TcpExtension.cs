using AsyncNet.Tcp.Client;
using AsyncNet.Tcp.Server;
using Microsoft.Samples.BizTalk.GenericAdapter.Contracts;
using Microsoft.Samples.BizTalk.GenericAdapter.Core;
using Microsoft.ServiceModel.Channels.Common;
using System;
using System.ServiceModel.Channels;
using Unity;
using Unity.Extension;
using Unity.Injection;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Tcp
{
    public class TcpExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            const string tcpConfiguration = "tcpconfiguration";

            Container.RegisterSingleton<IInboundServerConfiguration, TcpInboundServerConfiguration>(tcpConfiguration);
            Container.RegisterSingleton<IOutboundClientConfiguration, TcpOutboundClientConfiguration>(tcpConfiguration);

            var inboundConfiguration = Container.Resolve<IInboundServerConfiguration>(tcpConfiguration);
            var outboundConfiguration = Container.Resolve<IOutboundClientConfiguration>(tcpConfiguration);

            var serverConfig = new AsyncNetTcpServerConfig()
            {
                IPAddress = inboundConfiguration.IPAddress,
                Port = inboundConfiguration.Port
            };

            var clientConfig = new AsyncNetTcpClientConfig()
            {
                TargetHostname = outboundConfiguration.IPAddress.ToString(),
                TargetPort = outboundConfiguration.Port
            };

            Container.RegisterType<IAsyncNetTcpServer, AsyncNetTcpServer>(tcpConfiguration, new InjectionConstructor(serverConfig));
            Container.RegisterType<IAsyncNetTcpClient, AsyncNetTcpClient>(tcpConfiguration, new InjectionConstructor(clientConfig));

            Container.RegisterType<IInboundQueue<Tuple<bool, Message, IInboundReply>>, GenericAdapterInboundQueue>();

            Container.RegisterType<ApplicationMessage, XmlApplicationMessage>("XML");
            Container.RegisterType<ApplicationReply, GenericAdapterApplicationReply>();

            Container.RegisterType<GenericAdapterApplicationMessageFactory>(tcpConfiguration);
            Container.RegisterType<IInboundReplyFactory, GenericAdapterInboundReplyFactory>(tcpConfiguration);

            var inboundQueue = Container.Resolve<IInboundQueue<Tuple<bool, Message, IInboundReply>>>();

            Container.RegisterType<IInboundServer, TcpInboundServer>(tcpConfiguration,
                new InjectionConstructor(
                    new ResolvedParameter<IAsyncNetTcpServer>(tcpConfiguration),
                    inboundQueue,
                    new ResolvedParameter<ApplicationMessageFactory>(tcpConfiguration),
                    new ResolvedParameter<IInboundReplyFactory>(tcpConfiguration)));

            Container.RegisterType<IInboundHandlerService, GenericAdapaterInboundHandlerService>(tcpConfiguration,
                new InjectionConstructor(
                    new ResolvedParameter<IInboundServer>(tcpConfiguration),
                    inboundQueue),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>());

            Container.RegisterType<IInboundHandler, GenericAdapaterInboundHandler>(tcpConfiguration,
                new InjectionConstructor(new ResolvedParameter<IInboundHandlerService>(tcpConfiguration)));

            Container.RegisterType<IOutboundClient, TcpOutboundClient>(tcpConfiguration,
                new InjectionConstructor(
                    new ResolvedParameter<IAsyncNetTcpClient>(tcpConfiguration),
                    new ResolvedParameter<ApplicationMessageFactory>(tcpConfiguration)));

            Container.RegisterType<IOutboundHandlerService, GenericAdapaterOutboundHandlerService>(tcpConfiguration,
                new InjectionConstructor(
                    new ResolvedParameter<IOutboundClient>(tcpConfiguration)),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>());

            Container.RegisterType<IOutboundHandler, GenericAdapaterOutboundHandler>(tcpConfiguration,
                new InjectionConstructor(new ResolvedParameter<IOutboundHandlerService>(tcpConfiguration)));
        }
    }
}
