using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.UserData
{
    public interface IUserService
    {
        public Task<User> ValidateUser(string username, string password);
        public Task<User> RegisterUser(User user);
        public Task<User> EditUser(int id, string newPassword);
        public Task<List<User>> GetUser(string username);
        public Task<User> DeleteUser(int id);
    }
}