using System;

namespace Sep3Blazor.Data.Refresh
{
    /// <summary>
    /// Interface for RefreshServiceInvGroup
    /// </summary>
    public interface IRefreshServiceInvGroup
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}