using Microsoft.ServiceModel.Channels.Common;
/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterBinding.cs
/// Description :  This is the class used while creating a binding for an adapter
/// -----------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel.Channels;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterBinding : AdapterBinding
    {
        // Scheme in Binding does not have to be the same as Adapter Scheme. 
        // Over write this value as appropriate.
        const string BindingScheme = "genericbinding";

        /// <summary>
        /// Initializes a new instance of the AdapterBinding class
        /// </summary>
        public GenericAdapterBinding()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdapterBinding class with a configuration name
        /// </summary>
        public GenericAdapterBinding(string configName)
        {
            ApplyConfiguration(configName);
        }

        /// <summary>
        /// Applies the current configuration to the CustomAdapterBindingCollectionElement
        /// </summary>
        void ApplyConfiguration(string configurationName)
        {
            /*
            //
            // TODO : replace the <The config name of your adapter> below with the configuration name of your adapter
            //
            BindingsSection bindingsSection = (BindingsSection)System.Configuration.ConfigurationManager.GetSection("system.serviceModel/bindings");
            CustomAdapterBindingCollectionElement bindingCollectionElement = (CustomAdapterBindingCollectionElement)bindingsSection["<The config name of your adapter>"];
            CustomAdapterBindingElement element = bindingCollectionElement.Bindings[configurationName];
            if (element != null)
            {
                element.ApplyConfiguration(this);
            }
            */
            throw new NotImplementedException("ApplyConfiguration is not implemented");
        }




        GenericAdapter binding;



        /// <summary>
        /// Gets the URI transport scheme that is used by the channel and listener factories that are built by the bindings.
        /// </summary>
        public override string Scheme
        {
            get
            {
                return BindingScheme;
            }
        }

        /// <summary>
        /// Returns a value indicating whether this binding supports metadata browsing.
        /// </summary>
        public override bool SupportsMetadataBrowse
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Returns a value indicating whether this binding supports metadata retrieval.
        /// </summary>
        public override bool SupportsMetadataGet
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Returns a value indicating whether this binding supports metadata searching.
        /// </summary>
        public override bool SupportsMetadataSearch
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the custom type of the ConnectionUri.
        /// </summary>
        public override Type ConnectionUriType
        {
            get
            {
                return typeof(GenericAdapterConnectionUri);
            }
        }



        [System.Configuration.ConfigurationProperty("enableEventLog", DefaultValue = false)]
        public bool EnableEventLog { get; set; }

        [System.Configuration.ConfigurationProperty("channelName", DefaultValue = "", IsRequired = true)]
        public string ChannelName { get; set; }



        GenericAdapter BindingElement
        {
            get
            {
                if (binding == null)
                    binding = new GenericAdapter();
                binding.EnableEventLog = this.EnableEventLog;
                binding.ChannelName = this.ChannelName;
                return binding;
            }
        }



        /// <summary>
        /// Creates a clone of the existing BindingElement and returns it
        /// </summary>
        public override BindingElementCollection CreateBindingElements()
        {
            BindingElementCollection bindingElements = new BindingElementCollection();
            //Only create once
            bindingElements.Add(this.BindingElement);
            //Return the clone
            return bindingElements.Clone();
        }


    }
}
