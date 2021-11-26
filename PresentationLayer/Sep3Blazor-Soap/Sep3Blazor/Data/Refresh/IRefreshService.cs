using System;

namespace Sep3Blazor.Data.Refresh
{
    public interface IRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}