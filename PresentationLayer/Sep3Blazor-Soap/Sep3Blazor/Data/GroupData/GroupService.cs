using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;


namespace Sep3Blazor.Data.GroupData
{
    public class GroupService : IGroupService
    {
        private readonly String URL = "https://localhost:5004";
        public async Task<Notification> AddGroup(string groupName, int memberId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.PostGroupAsync(
                    new PostGroupRequest
                    {
                        GroupName = groupName,
                        MemberId = memberId
                    });
                Console.WriteLine("Greeting: " + reply.Message);
                return new Notification("Success", "Group "+groupName+" was successfully created. " , NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification("Error","Group "+groupName+" was not successfully added. " , NotificationType.Error);
            }
        }

      

        public async Task<Notification> DeleteGroup(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteGroupAsync(
                    new Request {Name = s});
                Console.WriteLine("Greeting: " + reply.Message);
                return new Notification("Success", "Group "+s+" was successfully deleted. " , NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification("Error","Group "+s+" was not successfully deleted. " , NotificationType.Error);
            }
        }

        public async Task<IList<Group>> GetGroupList(int groupId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetGroupMembersListAsync(
                    new Request {Name = groupId.ToString()});
                return JsonSerializer.Deserialize<List<Group>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return null;
            }
        }
    }
}