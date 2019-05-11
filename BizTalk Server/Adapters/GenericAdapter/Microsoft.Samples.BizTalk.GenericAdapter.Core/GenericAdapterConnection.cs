/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterConnection.cs
/// Description :  Defines the connection to the target system.
/// -----------------------------------------------------------------------------------------------------------

using Microsoft.ServiceModel.Channels.Common;
using System;
using Unity;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterConnection : IConnection
    {
        GenericAdapterConnectionFactory connectionFactory;
        string connectionId;

        /// <summary>
        /// Initializes a new instance of the CustomAdapterConnection class with the CustomAdapterConnectionFactory
        /// </summary>
        public GenericAdapterConnection(GenericAdapterConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
            this.connectionId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets the ConnectionFactory
        /// </summary>
        public GenericAdapterConnectionFactory ConnectionFactory
        {
            get
            {
                return this.connectionFactory;
            }
        }

        /// <summary>
        /// Closes the connection to the target system
        /// </summary>
        public void Close(TimeSpan timeout)
        {
            //
            //TODO: Implement physical closing of the connection
            //
        }

        /// <summary>
        /// Returns a value indicating whether the connection is still valid
        /// </summary>
        public bool IsValid(TimeSpan timeout)
        {
            //
            //TODO: Implement physical checking for the validity of the opened connection
            //
            return true;
        }

        /// <summary>
        /// Opens the connection to the target system.
        /// </summary>
        public void Open(TimeSpan timeout)
        {
            //
            //TODO: Implement physical opening of the connection
            //
        }

        /// <summary>
        /// Clears the context of the Connection. This method is called when the connection is set back to the connection pool
        /// </summary>
        public void ClearContext()
        {
            //
            //TODO: Implement clear context to set the connection back to the pool.
            //
        }

        /// <summary>
        /// Builds a new instance of the specified IConnectionHandler type
        /// </summary>
        public TConnectionHandler BuildHandler<TConnectionHandler>(MetadataLookup metadataLookup)
             where TConnectionHandler : class, IConnectionHandler
        {
            if (typeof(IOutboundHandler).IsAssignableFrom(typeof(TConnectionHandler)))
            {
                return Bootstrapper.Container.Resolve<TConnectionHandler>(this.ConnectionFactory.Adapter.ChannelName);
            }
            if (typeof(IInboundHandler).IsAssignableFrom(typeof(TConnectionHandler)))
            {
                return Bootstrapper.Container.Resolve<TConnectionHandler>(this.ConnectionFactory.Adapter.ChannelName);
            }
            if (typeof(IMetadataResolverHandler).IsAssignableFrom(typeof(TConnectionHandler)))
            {
                return new GenericAdapterMetadataResolverHandler() as TConnectionHandler;
            }
            if (typeof(IMetadataBrowseHandler).IsAssignableFrom(typeof(TConnectionHandler)))
            {
                return new GenericAdapterMetadataBrowseHandler() as TConnectionHandler;
            }
            if (typeof(IMetadataSearchHandler).IsAssignableFrom(typeof(TConnectionHandler)))
            {
                return new GenericAdapterMetadataSearchHandler() as TConnectionHandler;
            }

            return default(TConnectionHandler);
        }

        /// <summary>
        /// Aborts the connection to the target system
        /// </summary>
        public void Abort()
        {
            //
            //TODO: Implement abort logic. DO NOT throw an exception from this method
            //
        }

        /// <summary>
        /// Gets the Id of the Connection
        /// </summary>
        public String ConnectionId
        {
            get
            {
                return connectionId;
            }
        }
    }
}
