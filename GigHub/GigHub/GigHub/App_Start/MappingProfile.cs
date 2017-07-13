using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;
using GigHub.Core.DTOs;
using GigHub.Core.Models;

namespace GigHub.App_Start
{
    //AutoMapper has this concept Profile, where we store all related mappings
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification, NotificationDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<ApplicationUser, UserDto>();

            //Mapper.Initialize(cfg => cfg.CreateMap<Notification, NotificationDto>());
            //Mapper.Initialize(cfg => cfg.CreateMap<Gig, GigDto>());
            //Mapper.Initialize(cfg => cfg.CreateMap<ApplicationUser, UserDto>());
            //var cfg = new MapperConfigurationExpression();
            //cfg.CreateMap<Notification, NotificationDto>();
            //cfg.CreateMap<Gig, GigDto>();
            //cfg.CreateMap<ApplicationUser, UserDto>();
        }
    }
}