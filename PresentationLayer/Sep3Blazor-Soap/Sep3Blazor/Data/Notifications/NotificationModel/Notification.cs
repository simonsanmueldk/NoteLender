namespace Sep3Blazor.Data.Notifications.NotificationModel
{
    public class Notification
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }

        public Notification(string title, string content, NotificationType type)
        {
            Title = title;
            Content = content;
            Type = type;
            
        }
    }
}