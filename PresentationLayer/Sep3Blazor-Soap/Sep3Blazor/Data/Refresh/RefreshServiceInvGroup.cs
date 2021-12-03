using System;

namespace Sep3Blazor.Data.Refresh
{
    public class RefreshServiceInvGroup : IRefreshServiceInvGroup
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}