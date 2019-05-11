using Microsoft.ServiceModel.Channels.Common;
/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterConnectionUri.cs
/// Description :  This is the class for representing an adapter connection uri
/// -----------------------------------------------------------------------------------------------------------

using System;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    /// <summary>
    /// This is the class for building the CustomAdapterConnectionUri
    /// </summary>
    public class GenericAdapterConnectionUri : ConnectionUri
    {
        string portName = null;

        /// <summary>
        /// Initializes a new instance of the ConnectionUri class
        /// </summary>
        public GenericAdapterConnectionUri()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionUri class with a Uri object
        /// </summary>
        public GenericAdapterConnectionUri(Uri uri)
            : base()
        {
            Uri = uri;
        }

        public string PortName
        {
            get
            {
                return this.portName;
            }
            set
            {
                this.portName = value;
            }
        }

        /// <summary>
        /// Getter and Setter for the Uri
        /// </summary>
        public override Uri Uri
        {
            get
            {
                return new UriBuilder(GenericAdapter.SCHEME, "10.0.0.4", 8090).Uri;
            }

            set
            {
                this.portName = value.Host;
            }
        }
    }
}