using System;

namespace GrpcService.Model
{
    public class Group
    {
        /// <summary>
        /// Instance variables with get and set
        /// </summary>
        public int id { get; set; }
        public String name { get; set; }

        /// <summary>
        /// 2-argument constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Group(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}