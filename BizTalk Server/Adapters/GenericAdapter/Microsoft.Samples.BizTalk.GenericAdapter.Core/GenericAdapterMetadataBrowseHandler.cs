using Microsoft.ServiceModel.Channels;
using Microsoft.ServiceModel.Channels.Common;
/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterMetadataBrowseHandler.cs
/// Description :  This class is used while performing a connection-based browse for metadata from the target system.
/// -----------------------------------------------------------------------------------------------------------

using System;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public class GenericAdapterMetadataBrowseHandler : GenericAdapterHandlerBase, IMetadataBrowseHandler
    {
        /// <summary>
        /// Initializes a new instance of the CustomAdapterMetadataBrowseHandler class
        /// </summary>
        public GenericAdapterMetadataBrowseHandler()
        {
        }


        /// <summary>
        /// Retrieves an array of MetadataRetrievalNodes from the target system.
        /// The browse will return nodes starting from the childStartIndex in the path provided in absoluteName, and the number of nodes returned is limited by maxChildNodes.
        /// The method should complete within the specified timespan or throw a timeout exception.
        /// If absoluteName is null or an empty string, return nodes starting from the root + childStartIndex.
        /// If childStartIndex is zero, then return starting at the node indicated by absoluteName (or the root node if absoluteName is null or empty).
        /// </summary>
        public MetadataRetrievalNode[] Browse(string nodeId
            , int childStartIndex
            , int maxChildNodes, TimeSpan timeout)
        {
            //
            //TODO: Implement the metadata browse on the target system.
            //
            return null;
        }
    }
}
