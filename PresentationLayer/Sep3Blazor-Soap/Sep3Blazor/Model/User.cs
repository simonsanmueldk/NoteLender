namespace Sep3Blazor.Model
{
    public class User
    {
        private int id { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }

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