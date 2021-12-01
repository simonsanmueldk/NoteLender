using System;
using System.Text.Json.Serialization;

namespace GrpcService.Model
{
    public class User
    {
        public int id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String username { get; set; }
        public String password { get; set; }

       
        public User(int id, String firstName, String lastName, String username, String password)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
        }
      
    }
}