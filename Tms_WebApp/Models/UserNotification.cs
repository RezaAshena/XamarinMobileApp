using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tms_WebApp.Models
{
    public class UserNotification
    {
        public User User { get; set; }
        public Notification Notification  { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string  NotificationId { get; set; }

        public bool IsRead { get; set; }

        public UserNotification()
        {
        }
        public UserNotification(User user,Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if(notification == null)
                throw new ArgumentNullException("notification");

            User = user;
            Notification = notification;
        }
    }
}