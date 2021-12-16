using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.InvitationData
{
    /// <summary>
    /// Interface for InvitationService
    /// </summary>
    public interface IInvitationService
    {
        public Task<IList<Invitation>> GetInvitationList(int userId);
        public Task<Notification> AddInvitation(Invitation invitation);
        public Task<Notification> DeleteInvitation(int invitationId);
    }
}