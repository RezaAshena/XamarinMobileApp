using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tms_WebApp.Models;

namespace Tms_WebApp.Controllers.Api
{
    public class PicturesController : ApiController
    {
        private MyDbContext _context;

        public PicturesController()
        {
            _context = new MyDbContext();
        }

        //Get  api/Pictures
        public IEnumerable<Picture> GetPictures()
        {
            return _context.Pictures.ToList();
        }

        [Route("api/Pictures/{userid}")]
        public IEnumerable<Picture> GetPicturewByUserId(string userid)
        {
            var pic = _context.Pictures.Where(u => u.UserId == userid).ToList();

            if (pic == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return pic;
        }

        [HttpPost]
        public Picture CreatePicture(Picture pic)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Pictures.Add(pic);
            _context.SaveChanges();
            return pic;

        }


        [HttpDelete]
        [Route("api/Pictures/Delete/{id}")]
        public void DeletePicture(string id)
        {
            var pictureInDb = _context.Pictures.SingleOrDefault(p => p.Id == id);
            if (pictureInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Pictures.Remove(pictureInDb);
            _context.SaveChanges();

        }


        [HttpGet]
        [Route("api/Pictures/pic/{id}")]
        public Picture ImageGetById(string id)
        {
            var picindb = _context.Pictures.SingleOrDefault(p => p.Id == id);

            if (picindb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return picindb;
        }


        [HttpGet]
        [Route("api/Pictures/img/{id}")]
        public string GetUrlById(string id)
        {
            var picindb = _context.Pictures.SingleOrDefault(p => p.Id == id);

            if (picindb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return picindb.URL;
        }

        [HttpGet]
        [Route("api/Pictures/cnt/{id}")]
        public string GetContentById(string id)
        {
            var picindb = _context.Pictures.SingleOrDefault(p => p.Id == id);

            if (picindb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return picindb.Content;
        }

        [HttpGet]
        [Route("api/Pictures/cnt2/{id}")]
        public Image  GetcntById(string id)
        {
            var picindb = _context.Pictures.SingleOrDefault(p => p.Id == id);
            if (picindb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

           // Byte[] bytes = File.ReadAllBytes(picindb.Content);
            byte[] bytes = Convert.FromBase64String(picindb.Content);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;

        }
    }
}
