namespace Sep3Blazor.Data.Notifications.NotificationModel
{
    public class Notification
    {
        /// <summary>
        /// Instance variables
        /// </summary>
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }

        /// <summary>
        /// 3-argument constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        public Notification(string title, string content, NotificationType type)
        {
            Title = title;
            Content = content;
            Type = type;
            
        }
    }
}