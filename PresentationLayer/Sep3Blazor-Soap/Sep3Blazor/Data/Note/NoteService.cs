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


        public async Task<Note> GetNote(int groupId, int noteId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetNoteAsync(
                new Request {Name = groupId + "/" + noteId});
            Console.WriteLine("Greeting: " + reply.Message);
            Note note = JsonSerializer.Deserialize<Note>(reply.Message);
            return note;
        }

        public async Task<Note> AddNote(string path)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply = await client.PostNoteAsync(
                new Request {Name = path});
            Console.WriteLine("Greeting: " + reply.Message);
            Note note = JsonSerializer.Deserialize<Note>(reply.Message);
            Console.WriteLine(note.text);
            return note;
        }

        public async Task DeleteNote(string path)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.DeleteNoteAsync(
                new Request {Name = path});
            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}