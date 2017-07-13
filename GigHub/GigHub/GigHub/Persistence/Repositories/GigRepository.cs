using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendance
                .Where(u => u.AttendeeId == userId)
                .Select(g => g.Gig)
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigById(int id)
        {
            return _context.Gigs.Single(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs.Include(g => g.Attendees.Select(a => a.Attendee)).SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(a => a.ArtistId == userId &&
                a.DateTime > DateTime.Now &&
                !a.IsCancelled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithArtistGenreById(int id)
        {
            return _context.Gigs
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetUpcomingGigs()
        {
            return _context.Gigs
                .Include(a => a.Artist)
                .Include(b => b.Genre)
                .Where(g => !g.IsCancelled && g.DateTime > DateTime.Now);
        }
    }
}