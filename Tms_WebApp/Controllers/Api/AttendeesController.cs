using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Tms_WebApp.Models;

namespace Tms_WebApp.Controllers.Api
{

    public class AttendeesController : ApiController
    {
        private MyDbContext _context;

        public AttendeesController()
        {
            _context = new MyDbContext();
        }

        [HttpGet]
        public IEnumerable<Attendance> GetAttendees()
        {
            return _context.Attendances.ToList();
        }

        [HttpPost]
        public Attendance CreateAttendee (Attendance att)
        {
            //check for exist
            //var userId = App.user.Id;
            //var postId = att.PostId;
            //var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.PostId == postId);
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Attendances.Add(att);
            _context.SaveChanges();
            return att;
        }

        [HttpDelete]
        [Route("api/Attendees/{postId}/{attendeeId}")]
        public void DeleteAttend(string postId,string attendeeId)
        {
            var attendeeInDb = _context.Attendances.SingleOrDefault(a => a.PostId == postId && a.AttendeeId == attendeeId);
            if(attendeeInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Attendances.Remove(attendeeInDb);
            _context.SaveChanges();
        }

    }
}
