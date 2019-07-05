using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tms_WebApp.Models
{
    public class Post
    {
        public string Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Experience { get; set; }
        [StringLength(150)]
        public string AdminArea { get; set; }
        [StringLength(100)]
        public string CountryCode { get; set; }
        [StringLength(100)]
        public string CountryName { get; set; }
        [StringLength(100)]
        public string FeatureName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [StringLength(100)]
        public string Locality { get; set; }
        [StringLength(100)]
        public string PostalCode { get; set; }
        [StringLength(250)]
        public string SubAdminArea { get; set; }
        [StringLength(250)]
        public string SubLocality { get; set; }
        [StringLength(100)]
        public string SubThroughfare { get; set; }
        [StringLength(100)]
        public string Throughfare { get; set; }

        public DateTimeOffset CREATEDAT { get; set; }

        public User User { get; set; }
        public string UserId{ get; set; }

        public bool IsDeleted { get;  set; }

        public ICollection<Attendance> Attendances { get; set; }

        public Post()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsDeleted = true;

            var notification = new Notification(NotificationType.PostCanceled, this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}