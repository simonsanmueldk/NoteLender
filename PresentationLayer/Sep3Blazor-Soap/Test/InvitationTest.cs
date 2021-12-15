using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Sep3Blazor.Data.InvitationData;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Test
{
    [TestFixture]
    public class InvitationTest
    {
        private InvitationService _invitationService;
        [SetUp]
        public void Setup()
        {
            _invitationService = new InvitationService();
        }

        [Test]
        public Task<IList<Invitation>> GetInvitationList(string userId)
        {
            return null;
        }
        [Test]
        public Task<Notification> DeleteInvitation(string userId)
        {
            return null;
        }
        
        [Test]
        public void AddInvitationReturnsNewInvitation()
        {
            var invitation = new Invitation(9, 5, "", 4, "Sim", 6, "Dorin");
            Assert.AreEqual(invitation,_invitationService.AddInvitation(invitation));
            /*
            invitation = new Invitation(9, 5, "", 4, "Sim", 6, "Dorin");
            var temp = _invitationService.AddInvitation(invitation);
            */
        }
    } 
}
