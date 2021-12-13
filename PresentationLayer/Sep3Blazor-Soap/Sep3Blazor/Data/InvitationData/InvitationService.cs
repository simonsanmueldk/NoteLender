using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.InvitationData
{
    public class InvitationService : IInvitationService
    {
        private readonly String URL = "https://localhost:5004";

        public async Task<IList<Invitation>> GetInvitationList(int userId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetInvitationListAsync(
                    new Request {Name = userId.ToString()});
                return JsonSerializer.Deserialize<List<Invitation>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return null;
            }
        }
        public async Task<Notification> AddInvitation(Invitation invitation)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.PostInvitationAsync(new PostInvitationRequest
                    {
                        Id = invitation.id,
                        GroupId = invitation.groupId,
                        InviteeId = invitation.inviteeId,
                        InvitorId = invitation.invitorId,
                    }
                );
                Console.WriteLine("greetings" + reply.Message);
                return new Notification("Success", "Invitation was successfully sent", NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification("Error", "Invitation failed to be sent", NotificationType.Error);
            }
        }

       

        public async Task<Notification> DeleteInvitation(int invitationId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteInvitationAsync(
                    new Request
                    {
                        Name = invitationId.ToString()
                    }
                );
                Console.WriteLine("Delete: " + reply.Message);
                return new Notification("Success", "Invitation was declined", NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification("Error", "Invitation failed to be deleted", NotificationType.Error);
            }
        }
    }
}