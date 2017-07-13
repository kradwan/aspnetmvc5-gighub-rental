using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureUserAttendances(string userId)
        {
            return _context
                .Attendance
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public bool AttendigGigByUser(int id, string userId)
        {
            return _context
                    .Attendance
                    .Any(a => a.GigId == id && a.AttendeeId == userId);
        }

        public IEnumerable<Attendance> GetUserAttendances(string userId)
        {
            return _context
                .Attendance
                .Where(a => a.AttendeeId == userId)
                .ToList();
        }

        public Attendance GetGigUserAttendance(string userId, int gigId)
        {
            return _context.Attendance.SingleOrDefault(x => x.AttendeeId == userId && x.GigId == gigId);
        }

        public void Add(Attendance attendance)
        {
            _context.Attendance.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendance.Remove(attendance);
        }
    }
}