namespace Sep3Blazor.Model
{
    public class Invitation
    {
        public int id{ get; set; }
        public int invitorId { get; set; }
        public int inviteeId { get; set; }
        public int groupId { get; set; }
     

        public Invitation(int id, int invitorId, int inviteeId, int groupId)
        {
            this.id = id;
            this.invitorId = invitorId;
            this.inviteeId = inviteeId;
            this.groupId = groupId;
        }
    }
}