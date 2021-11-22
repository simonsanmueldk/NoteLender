using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data
{
    public interface IInvitationService
    {
        public Task<IList<Invitation>> GetInvitations(String userId);
        public Task<IList<Invitation>> AddInvitations(Invitation invitation);
        
    }
}