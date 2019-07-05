using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tms_WebApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTimeOffset CREATEDAT { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; }
        public User()
        {
            UserNotifications = new Collection<UserNotification>();
        }

        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));
        }
    }
}