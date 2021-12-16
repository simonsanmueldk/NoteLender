using System;

namespace Sep3Blazor.Model
{
    public class Invitation
    {
        /// <summary>
        /// Instance variables with get and set
        /// </summary>
        public int id { get; set; }
        public int groupId { get; set; }
        
        public String groupName { get; set; }
        public int inviteeId { get; set; }
        
        public String inviteeName { get; set; }
        public int invitorId { get; set; }
        
        public String invitorName { get; set; }

        /// <summary>
        /// 7-argmument constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <param name="inviteeId"></param>
        /// <param name="inviteeName"></param>
        /// <param name="invitorId"></param>
        /// <param name="invitorName"></param>
        public Invitation(int id, int groupId,String groupName, int inviteeId,String inviteeName, int invitorId,String invitorName)
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