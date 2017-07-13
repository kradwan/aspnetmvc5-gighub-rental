using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigById(int id);
        void Add(Gig gig);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        Gig GetGigWithArtistGenreById(int id);
        IEnumerable<Gig> GetUpcomingGigs();
    }
}