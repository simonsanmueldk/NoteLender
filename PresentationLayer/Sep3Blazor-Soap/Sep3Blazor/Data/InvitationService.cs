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
        public IList<Invitation> InvitationList { get; set; }

        public Task<IList<Invitation>> GetInvitations(int userId)
        {
            throw new NotImplementedException();
        }

        public Task AddInvitation(Invitation invitation)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply = await client.PostInvitation(
                new Request {Name = s});
            InvitationList = JsonSerializer.Deserialize<List<Invitation>>(reply.Message);
            return InvitationList;
        }   
        
    }
}