/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterTrace.cs
/// Description :  Implements adapter tracing
/// -----------------------------------------------------------------------------------------------------------



namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    // Use CustomAdapterUtilities.Trace in the code to trace the adapter
    public class GenericAdapterUtilities
    {
        ////
        //// Initializes a new instane of  Microsoft.ServiceModel.Channels.Common.AdapterTrace using the specified name for the source
        ////
        //static AdapterTrace trace = new AdapterTrace("CustomAdapter");
        //public static readonly string InterchangeIdKey = "http://schemas.microsoft.com/BizTalk/2003/system-properties#InterchangeID";
        //public static readonly string MessageIdKey = "http://schemas.microsoft.com/BizTalk/2003/system-properties#BizTalkMessageID";
        //public static readonly string ServiceInstanceIdKey = "http://schemas.microsoft.com/BizTalk/2003/system-properties#TransmitInstanceID";

        ///// <summary>
        ///// Gets the AdapterTrace
        ///// </summary>
        //public static AdapterTrace Trace
        //{
        //    get
        //    {
        //        return trace;
        //    }
        //}

        public static void LogEvent(string message)
        {
        }
    }
}

