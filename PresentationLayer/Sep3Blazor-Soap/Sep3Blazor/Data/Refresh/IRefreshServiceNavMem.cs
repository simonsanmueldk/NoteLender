using System;

namespace Sep3Blazor.Data.Refresh
{
    public interface IRefreshServiceNavMem
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}