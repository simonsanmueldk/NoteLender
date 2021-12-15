using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public interface IGroupService
    {
        
        public Task<IList<Note>> GetNoteList(String s);
        
        public Task<IList<Group>> GetGroupList(String s);
        
        public Task<IList<Note>> AddNote(String s);
    }
}