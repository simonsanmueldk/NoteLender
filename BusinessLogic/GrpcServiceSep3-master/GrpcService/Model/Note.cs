using System;

namespace GrpcService.Model
{
    public class Note
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int groupId { get; set; }
        public int week { get; set; }
        public int year { get; set; }
        public String name { get; set; }
        public String status { get; set; }
        public String text { get; set; }

        public Note(int id, int userId, int groupId, int week, int year, string name, string status, string text)
        {
            this.id = id;
            this.userId = userId;
            this.groupId = groupId;
            this.week = week;
            this.year = year;
            this.name = name;
            this.status = status;
            this.text = text;
        }
    }
}