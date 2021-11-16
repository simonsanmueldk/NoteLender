using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public class InvitationService: IInvitationService
    {
        private readonly String URL = "https://localhost:5004";

        public Task<IList<Invitation>> GetInvitations(int userId)
        {
            
        }

        public Task AddInvitations(Invitation invitation)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply = await client.PostInvitationAsync(
                new Request {Name = s});
            Console.WriteLine("Greeting: " + reply.Message);
            GroupList = JsonSerializer.Deserialize<List<Group>>(reply.Message);
            Console.WriteLine(GroupList[0]);
            return GroupList;
        }

        public Task RemoveInvitations(Invitation invitation)
        {
            
        }
    }
}