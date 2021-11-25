using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public class NoteService : INoteService
    {
        
        private readonly String URL = "https://localhost:5004";
        public IList<Note> NoteList { get; set; }
        public async Task<IList<Note>> GetNoteList(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetNoteAsync(
                new Request {Name = s});
            Console.WriteLine("Greeting: " + reply.Message);
            NoteList = JsonSerializer.Deserialize<List<Note>>(reply.Message);
            return NoteList;
        }
        
        public async Task<Note> AddNote(Note note)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            // Send note to GRPC service.
            var reply = await client.PostNoteAsync(
                new NoteRequest
                {
                    NoteId = note.id,
                    UserId = note.userId,
                    GroupId = note.groupId,
                    Week = note.week,
                    Year = note.year,
                    Name = note.name,
                    Status = note.status,
                    Text = note.text
                });
            Console.WriteLine("Greeting: " + reply.Message);
            
            // Return message from GRPC.
            //Note temp = JsonSerializer.Deserialize<Note>(reply.Message);
            return null;
        }

        public async Task<Note> DeleteNote(int noteId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply = await client.DeleteNoteAsync(
                new Request()
                {
                    Name = noteId.ToString()
                }
            );
            Console.WriteLine("Greeting: " + reply.Message);
            return null;
        }

        public async Task<Note> editNote(Note note)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply = await client.PutNoteAsync(
                new NoteRequest
                {
                    NoteId = note.id,
                    UserId = note.userId,
                    GroupId = note.groupId,
                    Week = note.week,
                    Year = note.year,
                    Name = note.name,
                    Status = note.status,
                    Text = note.text
                });
            Console.WriteLine("Greeting: " + reply.Message);

            return null;
        }
        
    }
}