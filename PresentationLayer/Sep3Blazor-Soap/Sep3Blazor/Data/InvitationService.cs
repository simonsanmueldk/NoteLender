using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public class InvitationService: IInvitationService
    {
        public Task<IList<Invitation>> GetInvitations(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddInvitations(Invitation invitation)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveInvitations(Invitation invitation)
        {
            throw new System.NotImplementedException();
        }
    }
}