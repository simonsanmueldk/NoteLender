using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupData
{
    public interface IGroupService
    {
        public Task<IList<Group>> GetGroupList(String s);

        public Task<Notification> AddGroup(string groupName, int memberId);

        public Task<Notification> DeleteGroup(string s);
        
        public Task<IList<Group>> GetGroupList(int groupId);
    }
}