using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupData
{
    /// <summary>
    /// Interface for GroupService
    /// </summary>
    public interface IGroupService
    {
        public Task<Notification> AddGroup(string groupName, int memberId);

        public Task<Notification> DeleteGroup(string s);

        public Task<IList<Group>> GetGroupList(int groupId);
    }
}