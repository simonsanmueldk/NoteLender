using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.NoteData
{
    public class NoteService : INoteService
    {
        private readonly String URL = "https://localhost:5004";

        public async Task<IList<Note>> GetNoteList(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetNoteAsync(
                    new Request {Name = s});
                return JsonSerializer.Deserialize<List<Note>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return null;
            }
        }

        public async Task AddNote(Note note)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
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
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
            }
        }

        public async Task DeleteNote(int noteId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteNoteAsync(
                    new Request()
                    {
                        Name = noteId.ToString()
                    }
                );
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
            }
        }

        public async Task EditNote(Note note)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
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
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
            }
        }
    }
}