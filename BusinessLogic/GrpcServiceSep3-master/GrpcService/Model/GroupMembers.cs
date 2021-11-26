using System;

namespace GrpcService.Model
{
    public class GroupMembers
    {
        public int Id{ get; set; }
        public int UserId{ get; set; }
        public String Username{ get; set; }
        public int GroupId{ get; set; }

        public GroupMembers(int id, int userId, string username, int groupId)
        {
            this.Id = id;
            this.UserId = userId;
            this.Username = username;
            this.GroupId = groupId;
        }
    }
}