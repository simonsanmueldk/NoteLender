using System;
using System.Threading.Tasks;
using Sep3Blazor.Data.Notifications.NotificationModel;

namespace Sep3Blazor.Data.Notifications
{
    public class NotificationManager
    {
        public  event Action<Notification> OnShow;
        public  event Action OnHide;

        public  async Task Show(Notification notification)
        {
            Console.WriteLine("show");

            OnShow?.Invoke(notification);
            await Task.Delay(3000);
            try
            {
                OnHide?.Invoke();
                Console.WriteLine("Hide");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        
        

    }
}