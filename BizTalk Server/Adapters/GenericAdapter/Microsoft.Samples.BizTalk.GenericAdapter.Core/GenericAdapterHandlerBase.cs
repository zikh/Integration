/// -----------------------------------------------------------------------------------------------------------
/// Module      :  GenericAdapterHandlerBase.cs
/// Description :  This is the base class for handlers used to store common properties/helper functions
/// -----------------------------------------------------------------------------------------------------------

using System;

namespace Microsoft.Samples.BizTalk.GenericAdapter.Core
{
    public abstract class GenericAdapterHandlerBase
    {
        public GenericAdapterHandlerBase()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

