using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.InvitationData
{
    public interface IInvitationService
    {
        public Task<IList<Invitation>> GetInvitations(string userId);
        public Task<Invitation> AddInvitations(Invitation invitation);
        public Task<object> DeleteInvitation(string userId);

        
    }
}