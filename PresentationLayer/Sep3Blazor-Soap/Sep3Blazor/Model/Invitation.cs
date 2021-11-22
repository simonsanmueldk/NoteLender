namespace Sep3Blazor.Model
{
    public class Invitation
    {
        public int id{ get; set; }
        public int invitor_id { get; set; }
        public int invitee_id { get; set; }
        public int group_id { get; set; }
     

        public Invitation(int id, int invitorId, int inviteeId, int groupId)
        {
            this.id = id;
            this.invitor_id = invitorId;
            this.invitee_id = inviteeId;
            this.group_id = groupId;
        }
    }
}