using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sep3Blazor.Data
{
    public interface IGroupService
    {
        
        public Task<IList<string>> GetGroups(String s);
        
        public Task<IList<string>> AddGroup(String s);
    }
}