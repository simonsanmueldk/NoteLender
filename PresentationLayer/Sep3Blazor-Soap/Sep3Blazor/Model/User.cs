using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Sep3Blazor.Model
{
    public class User
    {
        /// <summary>
        /// Instance variables with get and set
        /// </summary>
        public int id { get; set; }
        [Required] [StringLength(20)] 
        public String firstName { get; set; }
        [Required] [StringLength(20)] 
        public String lastName { get; set; }
        [Required] [StringLength(15)] 
        public String username { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 7,
            ErrorMessage = "Last name should be between 7 and 15 characters")]
        public String password { get; set; }
        
         /// <summary>
        /// 5-argument constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public User(int id, string firstName, string lastName, string username, string password)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
        }

    }
}