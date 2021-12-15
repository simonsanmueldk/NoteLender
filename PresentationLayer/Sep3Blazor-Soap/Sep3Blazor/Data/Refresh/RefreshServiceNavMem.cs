using System;

namespace Sep3Blazor.Data.Refresh
{
    public class RefreshServiceNavMem : IRefreshServiceNavMem
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}