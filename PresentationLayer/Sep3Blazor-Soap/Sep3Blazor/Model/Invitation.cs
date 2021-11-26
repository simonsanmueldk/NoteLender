namespace Sep3Blazor.Model
{
    public class Invitation
    {
        public int id { get; set; }
        public int groupId { get; set; }
        public int inviteeId { get; set; }
        public int invitorId { get; set; }

        public Invitation(int id, int groupId, int inviteeId, int invitorId)
        {
            this.id = id;
            this.groupId = groupId;
            this.inviteeId = inviteeId;
            this.invitorId = invitorId;
        }
    }
}