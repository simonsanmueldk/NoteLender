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
        /// <summary>
        /// Instance variables, uri defines localhost
        /// </summary>
        private readonly String URL = "https://localhost:5004";

        /// <summary>
        /// Method gets GroupList from the businessServer
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>List of groups</returns>
        public async Task<IList<Group>> GetGroupList(int groupId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetGroupListAsync(
                    new Request {Name = groupId.ToString()});
                return JsonSerializer.Deserialize<List<Group>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        
        /// <summary>
        /// Method adds group, and uses PostGroupAsync in the businessServer to save it.
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="memberId"></param>
        /// <returns>Notification</returns>
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
                return new Notification("Success", "Group " + groupName + " was successfully created. ",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "Group " + groupName + " was not successfully added. ",
                    NotificationType.Error);
            }
        }
        
        /// <summary>
        /// Method removes group and uses DeleteGroupAsync from the businessServer
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Notification</returns>
        public async Task<Notification> DeleteGroup(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteGroupAsync(
                    new Request {Name = s});
                return new Notification("Success", "Group " + s + " was successfully deleted. ",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "Group " + s + " was not successfully deleted. ",
                    NotificationType.Error);
            }
        }
    }
}