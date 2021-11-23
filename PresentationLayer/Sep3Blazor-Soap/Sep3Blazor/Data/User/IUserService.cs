using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public interface IUserService
    {
        public Task<User> ValidateLogin(string username, string password);
        public Task<User> RegisterUser(User user);

        public Task<User> EditUser(int id,string newPassword);
    }
}