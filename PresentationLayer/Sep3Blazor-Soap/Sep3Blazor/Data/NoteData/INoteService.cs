using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.NoteData
{
    public interface INoteService
    {
        public Task<IList<Note>> GetNoteList(String s);
        public Task AddNote(Note note);
        public Task DeleteNote(int id);
        public Task EditNote(Note note);
    }
}