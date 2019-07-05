using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Tms_WebApp.Models;

namespace Tms_WebApp.Controllers.Api
{
    public class PostsController : ApiController
    {
        private MyDbContext _context;
        public PostsController()
        {
            _context = new MyDbContext();
        }

        public IEnumerable<Post> GetPosts()
        {
            return _context.Posts.Where(p => p.IsDeleted == false).ToList();
        }

        [Route("api/Posts/{userid}")]
        public IEnumerable<Post> GetPostByUserId(string userid)
        {
            var post = _context.Posts.Where(u => u.UserId == userid).ToList();

            if (post == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return post;
        }

        [Route("api/Posts/GetbyId/{id}")]
        public IEnumerable<Post> GetPostById(string id)
        {
            var post = _context.Posts.Where(p => p.Id == id);

            if (post == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return post;
        }


        [HttpPost]
        public Post CreatePost(Post post)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Posts.Add(post);
            _context.SaveChanges();

            return post;
        }

        [HttpDelete]
        [Route("api/Posts/Delete/{id}")]
        public void DeletePost(string id)
        {
            try
            {
                var postInDb = _context.Posts.SingleOrDefault(p => p.Id == id);
                if (postInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                postInDb.IsDeleted = true;
                Guid guid = Guid.NewGuid();
                var notification = new Notification()
                {
                    Id = guid.ToString(),
                    DateTime = DateTime.Now,
                    Post = postInDb,
                    Type = NotificationType.PostCanceled
                };
                var attendees = _context.Attendances.Where(a => a.PostId == postInDb.Id).Select(a => a.Attendee).ToList();

                foreach (var attendee in attendees)
                {

                    var userNotification = new UserNotification
                    {
                        User = attendee,
                        Notification = notification
                    };
                    _context.UserNotifications.Add(userNotification);
                }

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }

        }



        [HttpPut]
        [Route("api/Posts/Update/{id}")]
        public void UpdatePost(string id)
        {
            try
            {
                var postInDb = _context.Posts.SingleOrDefault(p => p.Id == id);
                if (postInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {

                var m = ex.Message;
            }

        }

        [HttpGet]
        [Route("api/Posts/{postId}/{attendeeid}")]
        public bool GetPostAttendance(string postId, string attendeeid)
        {
            var isAttending = _context.Attendances.Any(a => a.PostId == postId && a.AttendeeId == attendeeid);
            return isAttending;
        }

    }
}
