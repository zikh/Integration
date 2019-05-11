using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel.Configuration;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    [RunInstaller(true)]
    public partial class GenericAdapterInstaller : Installer
    {
        private Assembly adapterAssembly;
        private Type bindingSectionType;
        private Type bindingElementExtensionType;
        const string INSTALLER_PARM_INSTALLDIR = "INSTALLDIR";
        const string BINDING_ASSEMBLY_NAME = "Microsoft.Samples.BizTalk.GenericAdapter.Core.GenericAdapter.dll";
        const string BINDINGELEM_NAME = "genericadapter";
        const string BINDINGELEM_TYPE = "Microsoft.Samples.BizTalk.GenericAdapter.Core.GenericAdapterBindingElementExtensionElement";
        const string BINDING_NAME = "genericbinding";
        const string BINDING_TYPE = "Microsoft.Samples.BizTalk.GenericAdapter.Core.GenericAdapterBindingCollectionElement";
        const string BINDING_SCHEME = "genericbinding";

        public GenericAdapterInstaller()
        {
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
            try
            {
                foreach (var key in Context.Parameters.Keys)
                {
                    System.IO.File.AppendAllText(@"C:\output.txt", $"{key}: {Context.Parameters[key.ToString()]}");
                }

                Debug.Assert(this.Context != null, "Context of this installation is null.");
                string path = this.Context.Parameters[INSTALLER_PARM_INSTALLDIR] + BINDING_ASSEMBLY_NAME;
                adapterAssembly = Assembly.LoadFrom(path);
                Debug.Assert(adapterAssembly != null, "Adapter assembly is null.");
                bindingSectionType = adapterAssembly.GetType(BINDING_TYPE, true);
                Debug.Assert(bindingSectionType != null, "Binding type is null.");
                bindingElementExtensionType = adapterAssembly.GetType(BINDINGELEM_TYPE, true);
                Debug.Assert(bindingElementExtensionType != null, "Binding element extension type is null.");
                AddMachineConfigurationInfo();
            }
            catch (Exception ex)
            {
                throw new InstallException("Error while adding adapter configuration information. " + ex.InnerException.Message);
            }
        }

        public void AddMachineConfigurationInfo()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenMachineConfiguration();
            Debug.Assert(config != null, "Machine.Config returned null");
            ServiceModelSectionGroup sectionGroup = config.GetSectionGroup("system.serviceModel") as ServiceModelSectionGroup;
            if (sectionGroup != null)
            {

                bool channelEndpointElementExists = false;
                ClientSection clientSection = sectionGroup.Client;
                foreach (ChannelEndpointElement elem in clientSection.Endpoints)
                {
                    if (elem.Binding.Equals(BINDING_NAME, StringComparison.OrdinalIgnoreCase) && elem.Name.Equals(BINDING_SCHEME, StringComparison.OrdinalIgnoreCase) && elem.Contract.Equals("IMetadataExchange", StringComparison.OrdinalIgnoreCase))
                    {
                        channelEndpointElementExists = true;
                        break;
                    }
                }
                if (!channelEndpointElementExists)
                {
                    Debug.WriteLine("Adding ChannelEndpointElement for : " + BINDING_NAME);
                    ChannelEndpointElement elem = new ChannelEndpointElement();
                    elem.Binding = BINDING_NAME;
                    elem.Name = BINDING_SCHEME;
                    elem.Contract = "IMetadataExchange";
                    sectionGroup.Client.Endpoints.Add(elem);
                    Debug.WriteLine("Added ChannelEndpointElement for : " + BINDING_NAME);
                }

                if (!sectionGroup.Extensions.BindingElementExtensions.ContainsKey(BINDINGELEM_NAME))
                {
                    ExtensionElement ext = new ExtensionElement(BINDINGELEM_NAME, bindingElementExtensionType.FullName + "," + bindingElementExtensionType.Assembly.FullName);
                    sectionGroup.Extensions.BindingElementExtensions.Add(ext);
                }

                if (!sectionGroup.Extensions.BindingExtensions.ContainsKey(BINDING_NAME))
                {
                    ExtensionElement ext = new ExtensionElement(BINDING_NAME, bindingSectionType.FullName + "," + bindingSectionType.Assembly.FullName);
                    sectionGroup.Extensions.BindingExtensions.Add(ext);
                }

                config.Save();
            }
            else throw new InstallException("Machine.Config doesn't contain system.serviceModel node");
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            try
            {
                RemoveMachineConfigurationInfo();
            }
            catch (Exception ex)
            {
                throw new InstallException("Error while removing adapter configuration information" + ex.InnerException.Message);
            }
        }

        public void RemoveMachineConfigurationInfo()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenMachineConfiguration();
            Debug.Assert(config != null, "Machine.Config returned null");
            ServiceModelSectionGroup sectionGroup = config.GetSectionGroup("system.serviceModel") as ServiceModelSectionGroup;
            ChannelEndpointElement elemToRemove = null;
            if (sectionGroup != null)
            {
                foreach (ChannelEndpointElement elem in sectionGroup.Client.Endpoints)
                {
                    if (elem.Binding.Equals(BINDING_NAME, StringComparison.OrdinalIgnoreCase) && elem.Name.Equals(BINDING_SCHEME, StringComparison.OrdinalIgnoreCase) && elem.Contract.Equals("IMetadataExchange", StringComparison.OrdinalIgnoreCase))
                    {
                        elemToRemove = elem;
                        break;
                    }
                }
                if (elemToRemove != null)
                {
                    Debug.WriteLine("Removing ChannelEndpointElement for : " + BINDING_NAME);
                    sectionGroup.Client.Endpoints.Remove(elemToRemove);
                    Debug.WriteLine("Removed ChannelEndpointElement for : " + BINDING_NAME);
                }
                if (sectionGroup.Extensions.BindingExtensions.ContainsKey(BINDING_NAME))
                {
                    sectionGroup.Extensions.BindingExtensions.RemoveAt(BINDING_NAME);
                }
                if (sectionGroup.Extensions.BindingElementExtensions.ContainsKey(BINDINGELEM_NAME))
                {
                    sectionGroup.Extensions.BindingElementExtensions.RemoveAt(BINDINGELEM_NAME);
                }
                config.Save();
            }
            else
            {
                throw new InstallException("Machine.Config doesn't contain system.serviceModel node");
            }
        }
    }
}
