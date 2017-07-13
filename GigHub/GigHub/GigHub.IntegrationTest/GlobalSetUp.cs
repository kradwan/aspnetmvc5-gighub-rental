using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTest
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();

            Seed();
        }

        /// <summary>
        /// Setup ApplicationDbContext from EF for the DB migration
        /// creates DB from the EF
        /// </summary>
        private static void MigrateDbToLatestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        /// <summary>
        /// Seeds static data which will be needed for future as Users
        /// As we don't want to recreate them every time
        /// </summary>
        public void Seed()
        {
            var context = new ApplicationDbContext();
            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser { UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-" });
            context.Users.Add(new ApplicationUser { UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-" });

            context.SaveChanges();
        }
    }
}
