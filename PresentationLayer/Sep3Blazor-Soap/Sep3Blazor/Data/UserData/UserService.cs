using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
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

            var reply = await client.ValidateUserAsync(
                new Request {Name = username, Type = password});
            if (reply != null)
            {
                Console.WriteLine("Greeting: " + reply.Message);
                User user = JsonSerializer.Deserialize<User>(reply.Message);
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> RegisterUser(User user)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            Console.WriteLine(user.username + "lalala");
            var reply = await client.RegisterUserAsync(
                new RegisterRequest
                {
                    Username = user.username, Password = user.password,
                    FirstName = user.firstName, LastName = user.lastName
                });
            Console.WriteLine("Greeting: " + reply);
            if (reply != null)
            {
                User temp = JsonSerializer.Deserialize<User>(reply.Message);
                return temp;
            }

            return null;
        }

        public async Task<User> EditUser(int id, string newPassword)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.EditUserAsync(new EditUserRequest
                {Id = id, NewPassword = newPassword});
            Console.WriteLine("Greeting: " + reply);
            if (reply != null)
            {
                
            }

            return null;
        }

        public async Task<List<User>> GetUser(string username)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.GetUserAsync(
                new GetUserRequest {Username = username});
            Console.WriteLine("User" + reply);
            if (reply != null)
            {
                return JsonSerializer.Deserialize<List<User>>(reply.Message);
            }

            return null;
        }

        public async Task<User> DeleteUser(int id)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            var reply = await client.DeleteUserAsync(
                new UserRequest()
                {
                    Id = id
                }
            );
            Console.WriteLine("Greeting: " + reply.Message);
            return null;
        }
    }
}