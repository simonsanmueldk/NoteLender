using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sep3Blazor.Data.Notifications.NotificationModel;
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

        public async Task<Notification> AddNote(Note note)
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
                return new Notification("Success", "Note " + note.name + " was successfully created",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification("Error",
                    "Note " + note.name + " was not successfully added. ", NotificationType.Error);
            }
        }

        public async Task<Notification> DeleteNote(int noteId)
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
                return new Notification("Success", "Note was successfully deleted",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification("Error", "Note failed to be removed from group", NotificationType.Error);
            }
        }

        public async Task<Notification> EditNote(Note note)
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
                return new Notification("Success", "Note was successfully edited",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification("Error", "Note failed to be edited", NotificationType.Error);
            }
        }
    }
}