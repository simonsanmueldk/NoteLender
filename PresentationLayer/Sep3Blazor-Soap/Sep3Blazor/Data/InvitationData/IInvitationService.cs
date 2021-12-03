using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.InvitationData
{
    public interface IInvitationService
    {
        public Task<IList<Invitation>> GetInvitationList(string userId);
        public Task AddInvitation(Invitation invitation);
        public Task DeleteInvitation(string userId);

        
    }
}