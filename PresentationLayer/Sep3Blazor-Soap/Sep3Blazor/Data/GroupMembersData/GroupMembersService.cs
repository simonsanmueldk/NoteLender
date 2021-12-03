using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.GroupMembersData
{
    public class GroupMembersService : IGroupMembersService
    {
        private readonly String URL = "https://localhost:5004";

        public async Task<IList<GroupMembers>> GetGroupMemberList(int groupId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetUserListAsync(
                    new Request {Name = groupId.ToString()});
                Console.WriteLine("Group: " + reply.Message);
                return JsonSerializer.Deserialize<List<GroupMembers>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return null;
            }
        }


        public Task<IList<GroupMembers>> GetGroupMembersList(int group_id)
        {
            throw new NotImplementedException();
        }

        public async Task<Notification> AddGroupMember(int groupId, int userId)
        {
            Console.WriteLine("G" + groupId + " U " + userId);
            using var channel = GrpcChannel.ForAddress(URL);
            try
            {
                var client = new BusinessServer.BusinessServerClient(channel);
                var reply = await client.AddGroupMemberAsync(
                    new AddGroupMemberRequest {GroupId = groupId, UserId = userId});
                Console.WriteLine("Group: " + reply.Message);
                return new Notification("Success", "User with Id= "+userId+" was successfully added to group with Id = "+groupId , NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification(e.Status.StatusCode+" " +" "+ e.Status.Detail,"User with Id= "+userId+" was not successfully added to group with Id = "+groupId , NotificationType.Error);
            }
        }

        public async Task<Notification> LeaveGroup(int groupId, int userId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.LeaveGroupAsync(
                    new DeleteGroupMemberRequest {GroupId = groupId, UserId = userId});
                Console.WriteLine("Group: " + reply.Message);
                return new Notification("Success", "User with Id= "+userId+"  successfully left group with Id = "+groupId , NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification(e.Status.StatusCode+" " +" "+ e.Status.Detail,"User with Id= "+userId+" failed to leave group with Id = "+groupId , NotificationType.Error);
            }
        }

        public async Task<Notification> DeleteGroupMember(int id)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteGroupMemberAsync(
                    new UserRequest
                    {
                        Id = id
                    }
                );
                Console.WriteLine("DeleteGroupMember " + reply.Message);
                return new Notification("Success", "User was successfully removed from group" , NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
                return new Notification(e.Status.StatusCode+" " +" "+ e.Status.Detail,"User failed to be removed from group" , NotificationType.Error);
            }
        }
    }
}