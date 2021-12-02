using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupData
{
    public class GroupService : IGroupService
    {
        private readonly String URL = "https://localhost:5004";
        public IList<Group> GroupList { get; set; }

        public async Task<IList<Group>> AddGroup(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.PostGroupAsync(
                new PostGroupRequest
                {
                    GroupName = groupName,
                    MemberId = memberId
                });
            Console.WriteLine("Greeting: " + reply.Message);
            return JsonSerializer.Deserialize<List<Group>>(reply.Message);
        }

        public async Task<IList<Group>> GetGroupList(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetGroupAsync(
                new Request {Name = s});
            Console.WriteLine("Group: " + reply.Message);
            return JsonSerializer.Deserialize<List<Group>>(reply.Message);
        }

        public async Task DeleteGroup(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.DeleteGroupAsync(
                new Request {Name = s});
            Console.WriteLine("Greeting: " + reply.Message);
            GroupList = JsonSerializer.Deserialize<List<Group>>(reply.Message);
            // return GroupList;
        }

        public async Task<IList<Group>> GetGroupList(int groupId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetGroupMembersListAsync(
                new Request {Name = groupId.ToString()});
            Console.WriteLine("Group: " + reply.Message);
            return JsonSerializer.Deserialize<List<Group>>(reply.Message);
        }
        
        
    }
}