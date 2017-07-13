using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    

    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public ICollection<Following> GetUserFolowers(string userId)
        {
            return _context.Followings
                .Where(f => f.Followee.Id == userId)
                .Select(f => f)
                .Include(f => f.Follower)
                .ToList();
        }

        public IEnumerable<ApplicationUser> GetUserFollowees(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList().OrderBy(f => f.Name);
        }

        public bool FollowingArtistByUser(string gigArtistId, string userId)
        {
            return _context
                    .Followings
                    .Any(f => f.FolloweeId == gigArtistId && f.FollowerId == userId);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public IEnumerable<Following> GetArtistFollowingByUser(string dtoFolloweeId, string getUserId)
        {
            return _context.Followings.Where(f => f.FolloweeId == dtoFolloweeId && f.FollowerId == getUserId);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}