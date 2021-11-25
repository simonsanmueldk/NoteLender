using System;

namespace Sep3Blazor.Data.Refresh
{
    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}