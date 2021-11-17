using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public interface INoteService
    {
        public Task<Note> GetNote(int groupId, int noteId);
        public Task<Note> AddNote(string path);
        public Task DeleteNote(string path);
        
    }
}