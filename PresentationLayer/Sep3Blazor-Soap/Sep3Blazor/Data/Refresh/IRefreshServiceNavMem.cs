using System;

namespace Sep3Blazor.Data.Refresh
{
    /// <summary>
    /// Interface for RefreshServiceNavMem
    /// </summary>
    public interface IRefreshServiceNavMem
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}