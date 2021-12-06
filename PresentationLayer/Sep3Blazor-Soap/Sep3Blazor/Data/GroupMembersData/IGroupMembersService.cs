using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupMembersData
{
    public interface IGroupMembersService
    {
       
        public Task<Notification> AddGroupMember(int groupId, int userId);
        public Task<Notification> LeaveGroup(int groupId, int userId);
        public Task<Notification> DeleteGroupMember(int id);
        
        public Task<IList<GroupMembers>> GetGroupMemberList(int groupId);
    }
}