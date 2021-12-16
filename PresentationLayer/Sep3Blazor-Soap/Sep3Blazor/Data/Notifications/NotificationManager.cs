using System;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;

namespace Sep3Blazor.Data.Notifications
{
    public class NotificationManager
    {
        /// <summary>
        /// events OnShow and OnHide
        /// </summary>
        public  event Action<Notification> OnShow;
        public  event Action OnHide;

        /// <summary>
        /// Method for showing the notifications
        /// </summary>
        /// <param name="notification"></param>
        public  async Task Show(Notification notification)
        {
            OnShow?.Invoke(notification);
            await Task.Delay(3000);
            try
            {
                OnHide?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}