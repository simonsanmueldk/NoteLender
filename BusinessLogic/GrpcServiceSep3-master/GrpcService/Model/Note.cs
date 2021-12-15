using System;

namespace GrpcService.Model
{
    public class Note
    {
        /// <summary>
        /// Instance variables with get and set
        /// </summary>
        public int id { get; set; }
        public int groupId { get; set; }
        public int week { get; set; }
        public int year { get; set; }
        public String name { get; set; }
        public String status { get; set; }
        public String text { get; set; }

        /// <summary>
        /// 7-argument constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupId"></param>
        /// <param name="week"></param>
        /// <param name="year"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <param name="text"></param>
        public Note(int id, int groupId, int week, int year, string name, string status, string text)
        {
            this.id = id;
            this.groupId = groupId;
            this.week = week;
            this.year = year;
            this.name = name;
            this.status = status;
            this.text = text;
        }
    }
}