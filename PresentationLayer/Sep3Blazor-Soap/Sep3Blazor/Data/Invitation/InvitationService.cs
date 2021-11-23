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
            
            Console.WriteLine("ID: " + invitation.id + " InviteeId: " + invitation.inviteeId +
                              "InvitorId: " + invitation.invitorId + "Group: " + invitation.groupId);

            var reply = await client.PostInvitationAsync(new RegisterInvitationRequest
                {
                    Id = invitation.id,
                    InviteeId = invitation.inviteeId,
                    InvitorId = invitation.invitorId,
                    GroupId = invitation.groupId
                }
            );
            Console.WriteLine("greetings" + reply.Message);
            return null;

            /*
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply =  await client.PostInvitationAsync(
                new Request {Name = invitation});
            Console.WriteLine("mbappe: " + reply.Message);
            InvitationList = JsonSerializer.Deserialize<List<Invitation>>(reply.Message);
            Console.WriteLine(InvitationList[0]);
            return InvitationList;
            */
        }

        public async Task<IList<Invitation>> GetInvitations(String userId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetNoteAsync(
                new Request {Name = userId});
            Console.WriteLine("haaland: " + reply.Message);
            InvitationList = JsonSerializer.Deserialize<List<Invitation>>(reply.Message);
            return InvitationList;
        }
    }
}