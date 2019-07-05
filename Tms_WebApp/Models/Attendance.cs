using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tms_WebApp.Models
{
    public class Attendance
    {
        public User Attendee { get; set; }
        public Post Post { get; set; }

        [Key]
        [Column(Order =1)]
        public string PostId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }

    }
}