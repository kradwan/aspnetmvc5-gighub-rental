using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    /*
     * Implementation of UnitOfWork
     * 
     * This class recives context and is tigthly coupled to EntityFramework == ApplicationDbContext
     * 
     * In UnitOfWork we have always one or more repositories, it doesn't make sense to work without a repo
     */
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAttendanceRepository Attendance { get; set; }

        //private readonly GigsRepository _gigsRepository; // was directly inside a controller
        public IGigRepository Gigs { get; set; }
        public IFollowingRepository Following { get; set; }
        public IGenreRepository Genres { get; set; }
        public IUserNotificationRepository UserNotifications { get; set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Attendance = new AttendanceRepository(_context);
            Gigs = new GigRepository(_context);
            Following = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}