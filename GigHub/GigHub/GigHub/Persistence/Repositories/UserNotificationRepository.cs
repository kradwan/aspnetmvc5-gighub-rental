using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Repositories;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public UserNotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Notification> GetNewUserNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                    .Select(un => un.Notification)
                    .Include(un => un.Gig.Artist)
                    .ToList();
        }
    }
}