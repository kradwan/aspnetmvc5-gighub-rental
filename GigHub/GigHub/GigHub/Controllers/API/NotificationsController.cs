using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Core.DTOs;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetUserNotifications()
        {
            var userId = User.Identity.GetUserId();
            var userNotifications = _unitOfWork.UserNotifications.GetNewUserNotifications(userId);
                
            return userNotifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}