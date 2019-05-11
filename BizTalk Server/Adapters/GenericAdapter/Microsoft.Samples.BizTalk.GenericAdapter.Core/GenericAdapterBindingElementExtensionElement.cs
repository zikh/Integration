/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterBindingElementExtensionElement.cs
/// Description :  This class is provided to surface Adapter as a binding element, so that it 
///                can be used within a user-defined WCF "Custom Binding".
///                In configuration file, it is defined under
///                <system.serviceModel>
///                  <extensions>
///                     <bindingElementExtensions>
///                         <add name="{name}" type="{this}, {assembly}"/>
///                     </bindingElementExtensions>
///                  </extensions>
///                </system.serviceModel>
/// -----------------------------------------------------------------------------------------------------------



namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    using System;
    using System.Configuration;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Configuration;

    public class GenericAdapterBindingElementExtensionElement : BindingElementExtensionElement
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GenericAdapterBindingElementExtensionElement()
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
        /// Return the type of the adapter (binding element)
        /// </summary>
        public override Type BindingElementType
        {
            get
            {
                return typeof(GenericAdapter);
            }
        }
        /// <summary>
        /// Returns a collection of the configuration properties
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                ConfigurationPropertyCollection configProperties = base.Properties;
                configProperties.Add(new ConfigurationProperty("EnableEventLog", typeof(System.Boolean), false, null, null, ConfigurationPropertyOptions.None));
                configProperties.Add(new ConfigurationProperty("ChannelName", typeof(System.String), null, null, null, ConfigurationPropertyOptions.None));
                return configProperties;
            }
        }

        /// <summary>
        /// Instantiate the adapter.
        /// </summary>
        /// <returns></returns>
        protected override BindingElement CreateBindingElement()
        {
            GenericAdapter adapter = new GenericAdapter();
            this.ApplyConfiguration(adapter);
            return adapter;
        }

        /// <summary>
        /// Apply the configuration properties to the adapter.
        /// </summary>
        /// <param name="bindingElement"></param>
        public override void ApplyConfiguration(BindingElement bindingElement)
        {
            base.ApplyConfiguration(bindingElement);
            GenericAdapter adapterBinding = ((GenericAdapter)(bindingElement));
            adapterBinding.EnableEventLog = (System.Boolean)this["EnableEventLog"];
            adapterBinding.ChannelName = (System.String)this["ChannelName"];
        }

        /// <summary>
        /// Initialize the binding properties from the adapter.
        /// </summary>
        /// <param name="bindingElement"></param>
        protected override void InitializeFrom(BindingElement bindingElement)
        {
            base.InitializeFrom(bindingElement);
            GenericAdapter adapterBinding = ((GenericAdapter)(bindingElement));
            this["EnableEventLog"] = adapterBinding.EnableEventLog;
            this["ChannelName"] = adapterBinding.ChannelName;
        }

        /// <summary>
        /// Copy the properties to the custom binding
        /// </summary>
        /// <param name="from"></param>
        public override void CopyFrom(ServiceModelExtensionElement from)
        {
            base.CopyFrom(from);
            GenericAdapterBindingElementExtensionElement adapterBinding = ((GenericAdapterBindingElementExtensionElement)(from));
            this["EnableEventLog"] = adapterBinding.EnableEventLog;
            this["ChannelName"] = adapterBinding.ChannelName;
        }
    }
}

