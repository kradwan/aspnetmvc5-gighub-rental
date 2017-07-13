using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendance { get; set; }
        IGigRepository Gigs { get; set; }
        IFollowingRepository Following { get; set; }
        IGenreRepository Genres { get; set; }
        IUserNotificationRepository UserNotifications { get; set; }
        void Complete();
    }
}