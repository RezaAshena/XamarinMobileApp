using System;
using System.ComponentModel.DataAnnotations;

namespace Tms_WebApp.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public DateTime DateTime { get;  set; }
        public NotificationType Type { get;  set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalPlace { get; set; }
        public float? OriginalPrice { get; set; }

        [Required]
        public Post Post { get; set; }

        public Notification()
        {
        }

        public Notification (NotificationType type,Post post)
        {
            if(post == null)
                throw new ArgumentNullException("post");

            Guid guid = Guid.NewGuid();
            Id = guid.ToString();
            Type = type;
            Post = post;
            DateTime = DateTime.Now;
        }

    }
}