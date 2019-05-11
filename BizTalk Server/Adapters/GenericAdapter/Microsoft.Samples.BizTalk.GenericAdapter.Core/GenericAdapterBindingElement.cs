/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterBindingElement.cs
/// Description :  Provides a base class for the configuration elements.
/// -----------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterBindingElement : StandardBindingElement
    {
        ConfigurationPropertyCollection properties;


        /// <summary>
        /// Initializes a new instance of the CustomAdapterBindingElement class
        /// </summary>
        public GenericAdapterBindingElement()
            : base(null)
        {
        }


        /// <summary>
        /// Initializes a new instance of the CustomAdapterBindingElement class with a configuration name
        /// </summary>
        public GenericAdapterBindingElement(string configurationName)
            : base(configurationName)
        {
        }



        [System.Configuration.ConfigurationProperty("enableEventLog", DefaultValue = false)]
        public bool EnableEventLog
        {
            get
            {
                return ((bool)(base["EnableEventLog"]));
            }
            set
            {
                base["EnableEventLog"] = value;
            }
        }

        [System.Configuration.ConfigurationProperty("channelName", DefaultValue = "", IsRequired = true)]
        public string ChannelName
        {
            get
            {
                return ((string)(base["ChannelName"]));
            }
            set
            {
                base["ChannelName"] = value;
            }
        }



        /// <summary>
        /// Gets the type of the BindingElement
        /// </summary>
        protected override Type BindingElementType
        {
            get
            {
                return typeof(GenericAdapterBinding);
            }
        }



        /// <summary>
        /// Initializes the binding with the configuration properties
        /// </summary>
        protected override void InitializeFrom(Binding binding)
        {
            base.InitializeFrom(binding);
            GenericAdapterBinding adapterBinding = (GenericAdapterBinding)binding;
            this["EnableEventLog"] = adapterBinding.EnableEventLog;
            this["ChannelName"] = adapterBinding.ChannelName;
        }

        /// <summary>
        /// Applies the configuration
        /// </summary>
        protected override void OnApplyConfiguration(Binding binding)
        {
            if (binding == null)
                throw new ArgumentNullException("binding");

            GenericAdapterBinding adapterBinding = (GenericAdapterBinding)binding;
            adapterBinding.EnableEventLog = (System.Boolean)this["EnableEventLog"];
            adapterBinding.ChannelName = (System.String)this["ChannelName"];
        }

        /// <summary>
        /// Returns a collection of the configuration properties
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                if (this.properties == null)
                {
                    ConfigurationPropertyCollection configProperties = base.Properties;
                    configProperties.Add(new ConfigurationProperty("EnableEventLog", typeof(System.Boolean), false, null, null, ConfigurationPropertyOptions.None));
                    configProperties.Add(new ConfigurationProperty("ChannelName", typeof(System.String), null, null, null, ConfigurationPropertyOptions.None));
                    this.properties = configProperties;
                }
                return this.properties;
            }
        }


    }
}
