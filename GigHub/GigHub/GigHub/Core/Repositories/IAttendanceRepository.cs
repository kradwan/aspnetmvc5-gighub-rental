using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureUserAttendances(string userId);
        bool AttendigGigByUser(int id, string userId);
        IEnumerable<Attendance> GetUserAttendances(string userId);
        Attendance GetGigUserAttendance(string userId, int gigId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}