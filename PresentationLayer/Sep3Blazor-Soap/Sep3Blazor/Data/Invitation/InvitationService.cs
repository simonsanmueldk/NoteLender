using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Sep3Blazor.Model;


namespace Sep3Blazor.Data
{
    public class InvitationService : IInvitationService
    {
        private readonly String URL = "https://localhost:5004";
        public IList<Invitation> InvitationList { get; set; }

        public async Task<Invitation> AddInvitations(Invitation invitation)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            
            Console.WriteLine("ID: " + invitation.id + "Group: " + invitation.groupId, " InviteeId: " + invitation.inviteeId +
                              "InvitorId: " + invitation.invitorId );

            var reply = await client.PostInvitationAsync(new RegisterInvitationRequest
                {
                    Id = invitation.id,
                    GroupId = invitation.groupId,
                    InviteeId = invitation.inviteeId,
                    InvitorId = invitation.invitorId,
                }
            );
            Console.WriteLine("greetings" + reply.Message);
            return null;
        }

        public async Task<IList<Invitation>> GetInvitationList(string userId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetInvitationListAsync(
                new Request {Name = userId});
            Console.WriteLine("haaland: " + reply.Message);
            InvitationList = JsonSerializer.Deserialize<List<Invitation>>(reply.Message);
            return InvitationList;
        }
        
        public async Task DeleteInvitation(string userId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.DeleteInvitationAsync(
                new Request {Name = userId});
            Console.WriteLine("Greeting: " + reply.Message);
            InvitationList = JsonSerializer.Deserialize<List<Invitation>>(reply.Message);    
        }
    }
}