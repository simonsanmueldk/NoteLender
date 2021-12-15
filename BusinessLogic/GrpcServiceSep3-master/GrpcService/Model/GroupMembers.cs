using System;

namespace GrpcService.Model
{
    public class GroupMembers
    {
        /// <summary>
        /// Instance variables with get and set
        /// </summary>
        public int id { get; set; }
        public int userId { get; set; }
        public String username { get; set; }
        public int groupId { get; set; }

        /// <summary>
        /// 4-argument constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="groupId"></param>
        public GroupMembers(int id, int userId, string username, int groupId)
        {
            this.id = id;
            this.userId = userId;
            this.username = username;
            this.groupId = groupId;
        }
    }
}