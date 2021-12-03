using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
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

        public async Task AddGroupMember(int groupId, int userId)
        {
            Console.WriteLine("G" + groupId + " U " + userId);
            using var channel = GrpcChannel.ForAddress(URL);
            try
            {
                var client = new BusinessServer.BusinessServerClient(channel);
                var reply = await client.AddGroupMemberAsync(
                    new AddGroupMemberRequest {GroupId = groupId, UserId = userId});
                Console.WriteLine("Group: " + reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
            }
        }

        public async Task LeaveGroup(int group_id, int user_id)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.LeaveGroupAsync(
                    new DeleteGroupMemberRequest {GroupId = group_id, UserId = user_id});
                Console.WriteLine("Group: " + reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
            }
        }

        public async Task DeleteGroupMember(int id)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteGroupMemberAsync(
                    new UserRequest()
                    {
                        Id = id
                    }
                );
                Console.WriteLine("DeleteGroupMember " + reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
                Console.WriteLine(e.Status.StatusCode);
                Console.WriteLine((int) e.Status.StatusCode);
            }
        }
    }
}