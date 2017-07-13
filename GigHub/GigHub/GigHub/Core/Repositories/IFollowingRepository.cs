using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        ICollection<Following> GetUserFolowers(string userId);
        IEnumerable<ApplicationUser> GetUserFollowees(string userId);
        bool FollowingArtistByUser(string gigArtistId, string userId);
        void Add(Following following);
        IEnumerable<Following> GetArtistFollowingByUser(string dtoFolloweeId, string getUserId);
        void Remove(Following following);
    }
}