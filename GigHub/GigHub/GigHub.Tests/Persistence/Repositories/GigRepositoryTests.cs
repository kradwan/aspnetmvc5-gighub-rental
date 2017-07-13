using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Persistence.Repositories
{

    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _gigRepository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            //3. 
            //as this repo delivers Gigs
            //we need to get a reference to a gig property
            _mockGigs = new Mock<DbSet<Gig>>();

            //2.
            var mockContext = new Mock<IApplicationDbContext>();

            //4.
            //to initialize Gig DbSet
            //so that will return the mocked DbSet
            //in each test we need to populate this Mock DbSet with a different set of data 
            //call the repository method and assert that it returns the right piece of data
            mockContext.SetupGet(m => m.Gigs).Returns(_mockGigs.Object);
            //to mock DbSet we're going to grab some code from MSDN

            //1.
            _gigRepository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig }); //or new List<Gig>() { gig});

            var gigs = _gigRepository.GetUpcomingGigsByArtist("1");

            //test
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new List<Gig>() { gig });

            var gigs = _gigRepository.GetUpcomingGigsByArtist("1");
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForDifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            _mockGigs.SetSource(new[] { gig });

            var gigs = _gigRepository.GetUpcomingGigsByArtist(gig.ArtistId + "-");
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1", Genre = new Genre() { Id = 1, Name = "Jazz" } };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _gigRepository.GetUpcomingGigsByArtist("1");
            gigs.Should().Contain(gig);
            gigs.First().Genre.Should().NotBeNull();
            gigs.First().Genre.Name.Should().Be("Jazz");

        }

        [TestMethod]
        public void GetGigWithArtistGenreById_GigIsWithIdAndIncludesArtistGenre_ShouldBeReturned()
        {
            var gigsTmp = new[]
            {
                new Gig() { Id = 1, Artist = new ApplicationUser() { Id = "1", Name = "Nero" }, Genre = new Genre() { Id = 1, Name = "Jazz" } },
                new Gig() { Id = 2, Artist = new ApplicationUser() { Id = "2", Name = "Duper" }, Genre = new Genre() { Id = 2, Name = "Blues" } }
            };

            _mockGigs.SetSource(gigsTmp);

            var gig = _gigRepository.GetGigById(1);
            gig.Should().Be(gigsTmp[0]);
            gig.Id.Should().Be(1);
            gig.Artist.Id.Should().Be("1");
            gig.Artist.Name.Should().Be("Nero");
            gig.Genre.Id.Should().Be(1);
            gig.Genre.Name.Should().Be("Jazz");

        }

        [TestMethod]
        public void GetGigsUserAttending_ThereAreNoGigsForUser_ShouldNotReturn()
        {
            //var gigsTmp = new[]
            //{
            //    new Gig() { Id = 1, Attendees = new List<Attendance>() {new Attendance() {AttendeeId = "1"} },ArtistId = "1", Artist = new ApplicationUser() { Id = "1", Name = "Nero" }, Genre = new Genre() { Id = 1, Name = "Jazz" } },
            //    new Gig() { Id = 2, ArtistId = "1", Artist = new ApplicationUser() { Id = "1", Name = "Nero" }, Genre = new Genre() { Id = 2, Name = "Blues" } },
            //    new Gig() { Id = 3, ArtistId = "1", Artist = new ApplicationUser() { Id = "1", Name = "Nero" }, Genre = new Genre() { Id = 2, Name = "Blues" } }
            //};

            //_mockGigs.SetSource(gigsTmp);

            //var gigs = _gigRepository.GetGigsUserAttending("1-");
            //gigs.Should().BeNull();
        }
    }
}
