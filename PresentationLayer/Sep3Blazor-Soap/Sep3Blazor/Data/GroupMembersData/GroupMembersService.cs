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
        /// <summary>
        /// Instance variables, uri defines localhost
        /// </summary>
        private readonly String URL = "https://localhost:5004";

        /// <summary>
        /// Method leaveGroup removes user from group using the businessServer
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns>Notification</returns>
        public async Task<Notification> LeaveGroup(int groupId, int userId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.LeaveGroupAsync(
                    new DeleteGroupMemberRequest {GroupId = groupId, UserId = userId});
                return new Notification("Success",
                    "User with Id= " + userId + "  successfully left group with Id = " + groupId,
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error",
                    "User with Id= " + userId + " failed to leave group with Id = " + groupId, NotificationType.Error);
            }
        }
        /// <summary>
        /// Method shows group members using GetGroupMemberListAsync from businessServer
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Notification</returns>
        public async Task<IList<GroupMembers>> GetGroupMemberList(int groupId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetGroupMemberListAsync(
                    new Request {Name = groupId.ToString()});
                return JsonSerializer.Deserialize<List<GroupMembers>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Method adds group member, using AddGroupMemberAsync in businessServer
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Notification> AddGroupMember(int groupId, int userId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            try
            {
                var client = new BusinessServer.BusinessServerClient(channel);
                var reply = await client.AddGroupMemberAsync(
                    new AddGroupMemberRequest {GroupId = groupId, UserId = userId});
                return new Notification("Success", "User was successfully added to group", NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "User  was not successfully added to group", NotificationType.Error);
            }
        }

       

        /// <summary>
        /// Method removes group member using DeleteGroupMemberAsync from businessServer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                return new Notification("Success", "User was successfully removed from group",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "User failed to be removed from group", NotificationType.Error);
            }
        }
    }
}