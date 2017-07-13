using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Persistence.EntityConfigurations;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //In order to prevent On Cascade delete we need to use Fluent API
        //OnModelCreating maps tables' names and relations and default conventions
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //after creating new configuration class we need to reference it here
            modelBuilder.Configurations.Add(new GigConfiguration());

            //modelBuilder
            //    .Entity<Attendance>()
            //    .HasRequired(g => g.Gig)
            //    .WithMany(a => a.Attendees)
            //    .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<ApplicationUser>()
                .HasMany(f => f.Followees)
                .WithRequired(u => u.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<ApplicationUser>()
                .HasMany(u=> u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<UserNotification>()
                .HasRequired(u => u.User)
                .WithMany(n => n.UserNotifications) //added UserNotifications to the ApplicationUser class
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}