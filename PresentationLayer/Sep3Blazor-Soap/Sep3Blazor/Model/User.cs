using System;
using System.ComponentModel.DataAnnotations;

namespace Sep3Blazor.Model
{
    public class User
    {
        public int id { get; set; }
        [Required] [StringLength(20)] public String FirstName { get; set; }
        [Required] [StringLength(20)] public String LastName { get; set; }
        [Required] [StringLength(15)] public String Username { get; set; }
        [Required] /*[Range(7,15,ErrorMessage = "Please enter a value between 7-15 characters for Password")]*/ public String Password { get; set; }

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