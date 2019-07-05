using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tms_WebApp.Dto;
using Tms_WebApp.Models;


namespace Tms_WebApp.Controllers.Api
{
   
    public class UsersController : ApiController
    {
        private MyDbContext _Context;
        public UsersController()
        {
            _Context = new MyDbContext();
        }

        // Get /api/Users
        //public IEnumerable<UserDto> GetUsers()
        //{
        //    return _Context.Users.ToList().Select(Mapper.Map<User, UserDto>);
        //}

        public IEnumerable<User> GetUsers()
        {
            return _Context.Users.ToList();
        }

        //////Get /api/Users/1
        ////public UserDto GetUser(string id)
        ////{
        ////    var user = _Context.Users.SingleOrDefault(u => u.Id == id);

        ////    if (user == null)
        ////        throw new HttpResponseException(HttpStatusCode.NotFound);
        ////    return Mapper.Map<User,UserDto>(user);
        ////}
        ///
        public User GetUser(string id)
        {
            var user = _Context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return user;
        }

        //Get /api/Users/Email/{Email}/Password/{Password}
        [Route("api/Users/{email}/{pass}")]
        public User GetUserByMailPass(string email,string pass)
        {
            //var user = (_Context.Users.Where((u => u.Email == email) && (u.Password = pass)).ToList()).FirstOrDefault();
           var  user = (_Context.Users.Where(p => (p.Email == email) && (p.Password == pass)).ToList()).FirstOrDefault();
            //var user = (_Context.Users.Where(p => (p.Email == email)).ToList()).FirstOrDefault();
            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return user;

        }

        //////Post  /api/Users/
        ////[HttpPost]
        ////public UserDto CreateUser (UserDto userDto)
        ////{
        ////    if (!ModelState.IsValid)
        ////        throw new HttpResponseException(HttpStatusCode.BadRequest);

        ////    var user = Mapper.Map<UserDto, User>(userDto);
        ////    _Context.Users.Add(user);
        ////    _Context.SaveChanges();

        ////    userDto.Id = user.Id;

        ////    return userDto;
        ////}

        //Post  /api/Users/
        [HttpPost]
        public User CreateUser(User user)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _Context.Users.Add(user);
            _Context.SaveChanges();

            return user;
        }
        ////Put  /api/users/1
        //[HttpPut]
        //public void UpdateUser(string id,UserDto userDto)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var userInDb = _Context.Users.SingleOrDefault(u => u.Id == id);

        //    if (userInDb == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    Mapper.Map(userDto, userInDb);

        //    _Context.SaveChanges();
        //}

        //Put  /api/users/1
        [HttpPut]
        public void UpdateUser(string id, User user)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var userInDb = _Context.Users.SingleOrDefault(u => u.Id == id);

            if (userInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            userInDb.Email = user.Email;
            userInDb.Password = user.Password;
            userInDb.ConfirmPassword = user.ConfirmPassword;

            _Context.SaveChanges();
        }
        //Delete /api/users/1
        [HttpDelete]
        public void DeleteUser(string id)
        {
            var userInDb = _Context.Users.SingleOrDefault(u => u.Id == id);

            if (userInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _Context.Users.Remove(userInDb);

            _Context.SaveChanges();
        }
    }
}
