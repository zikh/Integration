using Microsoft.ServiceModel.Channels.Common;
using System.ServiceModel.Description;
/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterConnectionFactory.cs
/// Description :  Defines the connection factory for the target system.
/// -----------------------------------------------------------------------------------------------------------


namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterConnectionFactory : IConnectionFactory
    {

        // Stores the client credentials
        ClientCredentials clientCredentials;
        // Stores the adapter class
        GenericAdapter adapter;


        /// <summary>
        /// Initializes a new instance of the CustomAdapterConnectionFactory class
        /// </summary>
        public GenericAdapterConnectionFactory(ConnectionUri connectionUri
            , ClientCredentials clientCredentials
            , GenericAdapter adapter)
        {
            this.clientCredentials = clientCredentials;
            this.adapter = adapter;
        }


        /// <summary>
        /// Gets the adapter
        /// </summary>
        public GenericAdapter Adapter
        {
            get
            {
                return this.adapter;
            }
        }



        /// <summary>
        /// Creates the connection to the target system
        /// </summary>
        public IConnection CreateConnection()
        {
            return new GenericAdapterConnection(this);
        }

    }
}
