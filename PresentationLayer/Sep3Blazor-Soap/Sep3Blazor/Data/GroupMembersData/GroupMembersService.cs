using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupMembersData
{
    public class GroupMembersService : IGroupMembersService
    {
        private readonly String URL = "https://localhost:5004";

        public async Task<IList<GroupMembers>> GetUserList(int groupId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetUserListAsync(
                new Request {Name = groupId.ToString()});
            Console.WriteLine("Group: " + reply.Message);
            return JsonSerializer.Deserialize<List<GroupMembers>>(reply.Message);
        }
    }
}