using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Sep3Blazor.Model;


namespace Sep3Blazor.Data
{
    public class GroupService : IGroupService
    {
        private readonly String URL = "https://localhost:5004";
        public IList<Note> NoteList { get; set; }
        public IList<Group> GroupList { get; set; }
       

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

        public async Task<IList<Group>> AddGroup(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply = await client.PostGroupAsync(
                new Request {Name = s});
            Console.WriteLine("Greeting: " + reply.Message);
            GroupList = JsonSerializer.Deserialize<List<Group>>(reply.Message);
            Console.WriteLine(GroupList[0]);
            return GroupList;
        }
        
        public async Task<IList<Group>> GetGroupList(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetGroupAsync(
                new Request {Name = "1"});
            Console.WriteLine("Greeting: " + reply.Message);
            GroupList = JsonSerializer.Deserialize<List<Group>>(reply.Message);
            return GroupList;
        }
    }
}