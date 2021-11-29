using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupData
{
    public interface IGroupService
    {
        public Task<IList<Group>> GetGroupList(String s);

        public Task<IList<Group>> AddGroup(String s);

        public Task DeleteGroup(string s);
        
        public Task<IList<GroupMembers>> GetUserList(int group_id);
    }
}