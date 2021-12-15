using System;

namespace Sep3Blazor.Data.Refresh
{
    public interface IRefreshServiceInvGroup
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}