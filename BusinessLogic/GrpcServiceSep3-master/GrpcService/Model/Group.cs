using System;

namespace Sep3Blazor.Model
{
    public class Group
    {
        public int id{ get; set; }
        public String name{ get; set; }

        public Group(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}