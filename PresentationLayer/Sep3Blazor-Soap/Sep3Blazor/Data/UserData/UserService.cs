using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.UserData
{
    public class UserService : IUserService
    {
        private readonly String URL = "https://localhost:5004";

        public async Task<User> ValidateUser(string username, string password)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.ValidateUserAsync(
                    new Request {Name = username, Type = password});
                User user = JsonSerializer.Deserialize<User>(reply.Message);
                return user;
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Notification> RegisterUser(User user)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.RegisterUserAsync(
                    new RegisterRequest
                    {
                        Username = user.username, Password = user.password,
                        FirstName = user.firstName, LastName = user.lastName
                    });
                return new Notification("Success", "User "+user.username+" was successfully created",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error","User "+user.username+"failed to be created" , NotificationType.Error);
            }
        }
        
        public async Task<List<User>> GetUserList(string username)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetUserListAsync(
                    new GetUserRequest {Username = username});
                return JsonSerializer.Deserialize<List<User>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Notification> EditUser(int id, string newPassword)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.EditUserAsync(new EditUserRequest
                    {Id = id, NewPassword = newPassword});
                return new Notification("Success", "User was successfully edited",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error","User failed to be edited" , NotificationType.Error);
            }
        }

        

        public async Task<Notification> DeleteUser(int id)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteUserAsync(
                    new UserRequest
                    {
                        Id = id
                    }
                );
                return new Notification("Success", "User was successfully deleted",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                return new Notification("Error","User failed to be removed from group" , NotificationType.Error);
            }
        }
    }
}