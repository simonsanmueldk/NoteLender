using System;

namespace GrpcService.Model
{
    public class User
    {
        public int id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public User(int id, String firstName, String lastName, String username, String password)
        {
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
        }
    }
}