using System;
using Microsoft.AspNetCore.Components;
using Sep3Blazor.Data.Notifications;
using Sep3Blazor.Data.Notifications.NotificationModel;

namespace Sep3Blazor.Shared
{
    public class NotificationBase : ComponentBase, IDisposable
    {
        /// <summary>
        /// Instance variables
        /// </summary>
        [Inject] private NotificationManager NotificationManager { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }

        /// <summary>
        // Method to run, when user enters this page.
        /// </summary>
        protected override void OnInitialized()
        {
            NotificationManager.OnShow += Show;
            NotificationManager.OnHide += Hide;
        }

        /// <summary>
        /// Method to hide notification
        /// </summary>
        private void Hide()
        {
            IsVisible = false;
            InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Method to show notification
        /// </summary>
        /// <param name="notification"></param>
        private void Show(Notification notification)
        {
            Make(notification);
            IsVisible = true;
            InvokeAsync(StateHasChanged);
        }
        
        
        /// <summary>
        /// Method sets the types of notifications according to info, success or error
        /// </summary>
        /// <param name="notification"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            NotificationManager.OnShow -= Show;
        }
    }
}