using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public interface INoteService
    {
        public Task<IList<Note>> GetNoteList(String s);
        public Task<IList<Note>> AddNote(Note note);
    }
}