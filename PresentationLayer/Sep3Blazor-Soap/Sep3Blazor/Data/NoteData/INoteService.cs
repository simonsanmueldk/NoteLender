using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.NoteData
{
    public interface INoteService
    {
        public Task<IList<Note>> GetNoteList(String s);
        public Task<Notification> AddNote(Note note);
        public Task<Notification> DeleteNote(int id);
        public Task<Notification> EditNote(Note note);
    }
}