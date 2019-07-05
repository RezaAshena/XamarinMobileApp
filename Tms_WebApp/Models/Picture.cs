using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms_WebApp.Models
{
    public class Picture
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public String URL { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CREATEDAT { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}