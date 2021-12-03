using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.UserData
{
    public interface IUserService
    {
        public Task<User> ValidateUser(string username, string password);
        public Task<Notification> RegisterUser(User user);
        public Task<Notification> EditUser(int id, string newPassword);
        public Task<List<User>> GetUserList(string username);
        public Task<Notification> DeleteUser(int id);
    }
}