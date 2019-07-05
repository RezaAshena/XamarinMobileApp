using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms_WebApp.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}