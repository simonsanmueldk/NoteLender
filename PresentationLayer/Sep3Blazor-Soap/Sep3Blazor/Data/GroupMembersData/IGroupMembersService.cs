using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupMembersData
{
    public interface IGroupMembersService
    {
        public Task<IList<GroupMembers>> GetGroupMembersList(int group_id);
        public Task<IList<GroupMembers>> AddGroupMember(int groupId,  int userId);
        public Task<IList<GroupMembers>> DeleteGroupMembersList(int group_id, int user_id);
        
        
    }
}