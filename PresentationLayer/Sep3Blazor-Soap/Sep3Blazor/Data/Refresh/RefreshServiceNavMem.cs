using System;

namespace Sep3Blazor.Data.Refresh
{
    public class RefreshServiceNavMem : IRefreshServiceNavMem
    {
        /// <summary>
        /// Method occurs when an update of the content has been initiated
        /// </summary>
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}