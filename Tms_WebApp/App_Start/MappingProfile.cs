using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tms_Travel.Model;
using Tms_WebApp.Dto;

namespace Tms_WebApp.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();

        }
    }
}