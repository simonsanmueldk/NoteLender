using System;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public class UserService : IUserService
    {
        private readonly String URL = "https://localhost:5004";

        public async Task<User> ValidateLogin(string username, string password)
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

            Console.WriteLine(user.Username + "lalala");
            var reply = await client.RegisterUserAsync(
                new RegisterRequest
                {
                    Username = user.Username, Password = user.Password, FirstName = user.FirstName,
                    LastName = user.LastName
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
                User user = JsonSerializer.Deserialize<User>(reply.Message);
                return user;
            }

            return null;
        }
    }
}