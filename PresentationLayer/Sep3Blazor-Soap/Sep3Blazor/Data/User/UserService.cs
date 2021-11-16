using System;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public class UserService:IUserService
    {
        private readonly String URL = "https://localhost:5004";
        public async Task<User> ValidateLogin(string username, string password)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            var reply = await client.ValidateUserAsync(
                new Request {Name = username, Type = password});
            Console.WriteLine("Greeting: " + reply.Message);
            User user = JsonSerializer.Deserialize<User>(reply.Message);
            return user;
        }

        public async Task<User> RegisterUser(User user)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);

            Console.WriteLine(user.Username + "lalala");
            var reply = await client.RegisterUserAsync(
                new RegisterRequest{Username = user.Username, Password = user.Password,FirstName = user.FirstName,LastName = user.LastName});

            Console.WriteLine("Greeting: " + reply);
            User temp = JsonSerializer.Deserialize<User>(reply.Message);

            return null;
        }
    }
}