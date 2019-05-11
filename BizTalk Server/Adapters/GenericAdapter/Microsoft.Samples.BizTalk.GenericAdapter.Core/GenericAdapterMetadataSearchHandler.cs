using Microsoft.ServiceModel.Channels;
using Microsoft.ServiceModel.Channels.Common;
/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterMetadataSearchHandler.cs
/// Description :  This class is used for performing a connection-based search for metadata from the target system
/// -----------------------------------------------------------------------------------------------------------

using System;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterMetadataSearchHandler : GenericAdapterHandlerBase, IMetadataSearchHandler
    {
        /// <summary>
        /// Initializes a new instance of the CustomAdapterMetadataSearchHandler class
        /// </summary>
        public GenericAdapterMetadataSearchHandler()
        {
        }


        /// <summary>
        /// Retrieves an array of MetadataRetrievalNodes (see Microsoft.ServiceModel.Channels) from the target system.
        /// The search will begin at the path provided in absoluteName, which points to a location in the tree of metadata nodes.
        /// The contents of the array are filtered by SearchCriteria and the number of nodes returned is limited by maxChildNodes.
        /// The method should complete within the specified timespan or throw a timeout exception.  If absoluteName is null or an empty string, return nodes starting from the root.
        /// If SearchCriteria is null or an empty string, return all nodes.
        /// </summary>
        public MetadataRetrievalNode[] Search(string nodeId
            , string searchCriteria
            , int maxChildNodes, TimeSpan timeout)
        {
            //
            //TODO: Search for metadata on the target system.
            //
            return null;
        }
    }
}
