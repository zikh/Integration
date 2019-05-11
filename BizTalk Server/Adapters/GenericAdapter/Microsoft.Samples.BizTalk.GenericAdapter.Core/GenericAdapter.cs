using Microsoft.ServiceModel.Channels.Common;
/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapter.cs
/// Description :  The main adapter class which inherits from Adapter
/// -----------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel.Description;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapter : Adapter
    {
        // Scheme associated with the adapter
        internal const string SCHEME = "genericadapter";
        // Namespace for the proxy that will be generated from the adapter schema
        internal const string SERVICENAMESPACE = "adapter://genericadapter";
        // Initializes the AdapterEnvironmentSettings class
        static AdapterEnvironmentSettings environmentSettings = new AdapterEnvironmentSettings();

        /// <summary>
        /// Initializes a new instance of the CustomAdapter class
        /// </summary>
        public GenericAdapter()
            : base(environmentSettings)
        {
            Settings.Metadata.DefaultMetadataNamespace = SERVICENAMESPACE;
        }

        /// <summary>
        /// Initializes a new instance of the CustomAdapter class with a binding
        /// </summary>
        public GenericAdapter(GenericAdapter binding)
            : base(binding)
        {
            this.EnableEventLog = binding.EnableEventLog;
            this.ChannelName = binding.ChannelName;
        }



        [System.Configuration.ConfigurationProperty("enableEventLog", DefaultValue = false)]
        public bool EnableEventLog { get; set; }

        [System.Configuration.ConfigurationProperty("channelName", DefaultValue = "", IsRequired = true)]
        public string ChannelName { get; set; }


        /// <summary>
        /// Gets the URI transport scheme that is used by the adapter
        /// </summary>
        public override string Scheme
        {
            get
            {
                return SCHEME;
            }
        }



        /// <summary>
        /// Creates a ConnectionUri instance from the provided Uri
        /// </summary>
        protected override ConnectionUri BuildConnectionUri(Uri uri)
        {
            return new GenericAdapterConnectionUri(uri);
        }

        /// <summary>
        /// Builds a connection factory from the ConnectionUri and ClientCredentials
        /// </summary>
        protected override IConnectionFactory BuildConnectionFactory(
            ConnectionUri connectionUri
            , ClientCredentials clientCredentials
            , System.ServiceModel.Channels.BindingContext context)
        {
            return new GenericAdapterConnectionFactory(connectionUri, clientCredentials, this);
        }

        /// <summary>
        /// Returns a clone of the adapter object
        /// </summary>
        protected override Adapter CloneAdapter()
        {
            return new GenericAdapter(this);
        }

        /// <summary>
        /// Indicates whether the provided TConnectionHandler is supported by the adapter or not
        /// </summary>
        protected override bool IsHandlerSupported<TConnectionHandler>()
        {
            return (
                  typeof(IOutboundHandler) == typeof(TConnectionHandler)
                || typeof(IInboundHandler) == typeof(TConnectionHandler)
                || typeof(IMetadataResolverHandler) == typeof(TConnectionHandler)
                || typeof(IMetadataBrowseHandler) == typeof(TConnectionHandler)
                || typeof(IMetadataSearchHandler) == typeof(TConnectionHandler));
        }

        /// <summary>
        /// Gets the namespace that is used when generating schema and WSDL
        /// </summary>
        protected override string Namespace
        {
            get
            {
                return SERVICENAMESPACE;
            }
        }
    }
}
