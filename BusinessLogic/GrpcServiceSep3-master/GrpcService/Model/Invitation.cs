using System;

namespace GrpcService.Model
{
    public class Invitation
    {
        public int id { get; set; }
        public int groupId { get; set; }

        public String groupName { get; set; }
        public int inviteeId { get; set; }

        public String inviteeName { get; set; }
        public int invitorId { get; set; }

        public String invitorName { get; set; }

                //aaa
        public Invitation(int id, int groupId, String groupName, int inviteeId, String inviteeName, int invitorId,
            String invitorName)
        {
            this.id = id;
            this.groupId = groupId;
            this.groupName = groupName;
            this.inviteeId = inviteeId;
            this.inviteeName = inviteeName;
            this.invitorId = invitorId;
            this.invitorName = invitorName;
        }
    }
}