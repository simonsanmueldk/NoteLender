using System.Threading.Tasks;

namespace Sep3Blazor.Data
{
    public interface INoteService
    {
        public Task<Note> GetNote(string path);
        public Task<Note> AddNote(string path);
        public Task DeleteNote(string path);
        
    }
}