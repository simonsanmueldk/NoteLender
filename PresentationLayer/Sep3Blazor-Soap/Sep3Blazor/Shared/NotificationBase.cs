using System;
using Microsoft.AspNetCore.Components;
using Sep3Blazor.Data.Notifications;
using Sep3Blazor.Data.Notifications.NotificationModel;

namespace Sep3Blazor.Shared
{
    public class NotificationBase : ComponentBase, IDisposable
    {
        [Inject] private NotificationManager NotificationManager { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }

        public string Color { get; set; }
        public bool IsVisible { get; set; }

        protected override void OnInitialized()
        {
            NotificationManager.OnShow += Show;
            NotificationManager.OnHide += Hide;
        }

        private void Hide()
        {
            IsVisible = false;
            InvokeAsync(StateHasChanged);
        }

        private void Show(Notification notification)
        {
            Make(notification);
            IsVisible = true;
            InvokeAsync(StateHasChanged);
        }

        private void Make(Notification notification)
        {
            switch (notification.Type)
            {
                case NotificationType.Info:
                    Color = "yellow";
                    break;
                case NotificationType.Success:
                    Color = "green";
                    break;
                case NotificationType.Error:
                    Color = "red";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Title = notification.Title;
            Content = notification.Content;
        }

        public void Dispose()
        {
            NotificationManager.OnShow -= Show;
        }
    }
}