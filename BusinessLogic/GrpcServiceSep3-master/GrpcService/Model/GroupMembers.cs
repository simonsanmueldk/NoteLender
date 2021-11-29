using System;

namespace GrpcService.Model
{
    public class GroupMembers
    {
        public int id{ get; set; }
        public int userId{ get; set; }
        public String username{ get; set; }
        public int groupId{ get; set; }

        public GroupMembers(int id, int userId, string username, int groupId)
        {
            this.id = id;
            this.userId = userId;
            this.username = username;
            this.groupId = groupId;
        }
    }
}