namespace Sep3Blazor.Model
{
    public class Message
    {
        public string type{ get; set; }
        public string message{ get; set; }

        public Message(string message, string type)
        {
            this.message = message;
            this.type = type;
        }
    }
}