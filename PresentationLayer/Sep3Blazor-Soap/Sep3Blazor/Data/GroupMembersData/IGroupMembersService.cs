using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupMembersData
{
    public interface IGroupMembersService
    {
       
        public Task AddGroupMember(int groupId, int userId);
        public Task LeaveGroup(int group_id, int user_id);
        public Task DeleteGroupMember(int id);
        
        public Task<IList<GroupMembers>> GetUserList(int group_id);
    }
}