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
        /// <summary>
        /// Instance variables, uri defines localhost
        /// </summary>
        private readonly String URL = "https://localhost:5004";

        /// <summary>
        /// Method shows list of invitations using GetInvitationListAsync from businessServer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Notification</returns>
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
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// Method adds invitation using PostInvitationAsync from businesServer
        /// </summary>
        /// <param name="invitation"></param>
        /// <returns>Notification</returns>
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
                return new Notification("Success", "Invitation was successfully sent", NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "Invitation failed to be sent", NotificationType.Error);
            }
        }

        /// <summary>
        /// Method invitation removes invitation using DeleteInvitationAsync in the businessServer
        /// </summary>
        /// <param name="invitationId"></param>
        /// <returns>Notification</returns>
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
                return new Notification("Success", "Invitation was declined", NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "Invitation failed to be deleted", NotificationType.Error);
            }
        }
    }
}