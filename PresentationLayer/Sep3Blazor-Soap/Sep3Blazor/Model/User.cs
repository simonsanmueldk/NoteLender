using System;

namespace Sep3Blazor.Model
{
    public class User
    {
        public int id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public User(int id, string firstName, string lastName, string username, string password)
        {
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
        }
    }
}