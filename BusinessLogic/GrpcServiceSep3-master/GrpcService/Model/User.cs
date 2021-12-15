using System;
using System.Text.Json.Serialization;

namespace GrpcService.Model
{
    public class User
    {
        /// <summary>
        /// Instance variables with get and set
        /// </summary>
        public int id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        
        /// <summary>
        /// 5-argument constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
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