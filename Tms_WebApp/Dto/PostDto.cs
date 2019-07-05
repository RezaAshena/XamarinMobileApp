using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms_WebApp.Dto
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Experience { get; set; }
        public string VenueName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset CREATEDAT { get; set; }

    }
}