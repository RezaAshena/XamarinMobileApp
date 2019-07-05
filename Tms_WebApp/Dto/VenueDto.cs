using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms_WebApp.Dto
{
    public class VenueDto
    {
        public class Location
        {
            public string address { get; set; }
            public string crossStreet { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public int distance { get; set; }
            public string postalCode { get; set; }
            public string cc { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public IList<string> formattedAddress { get; set; }
        }

        public class Category
        {
            public string id { get; set; }
            public string name { get; set; }
            public string pluralName { get; set; }
            public string shortName { get; set; }
            public bool primary { get; set; }
        }

        public class Venue
        {
            public string id { get; set; }
            public string name { get; set; }
            public Location location { get; set; }
            public IList<Category> categories { get; set; }
        }
    }
}