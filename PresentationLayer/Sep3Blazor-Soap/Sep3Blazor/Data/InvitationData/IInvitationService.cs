using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.InvitationData
{
    public interface IInvitationService
    {
        public Task<IList<Invitation>> GetInvitationList(string userId);
        public Task<Notification> AddInvitation(Invitation invitation);
        public Task<Notification> DeleteInvitation(string userId);
    }
}