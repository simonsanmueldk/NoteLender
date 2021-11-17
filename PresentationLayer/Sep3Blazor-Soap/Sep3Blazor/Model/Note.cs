using System;

namespace Sep3Blazor.Model
{
    public class Note 

    {

        public int id{ get; set; }

        public int user_id { get; set; }

        public int group_id { get; set; }
    
        public int week{ get; set; }
 
        public int year{ get; set; }
  
        public String name{ get; set; }

        public String status{ get; set; }

        public String text{ get; set; }

        public Note(int id, int userId, int groupId, int week, int year, string name, string status, string text)
        {
            this.id = id;
            this.user_id = userId;
            this.group_id = groupId;
            this.week = week;
            this.year = year;
            this.name = name;
            this.status = status;
            this.text = text;
        }

        // public Note(int week, int year, string name, string status, string text)
        // {
        //     this.week = week;
        //     this.year = year;
        //     this.name = name;
        //     this.status = status;
        //     this.text = text;
        // }
    }
}