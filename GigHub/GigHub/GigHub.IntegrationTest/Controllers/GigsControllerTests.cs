using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationTest.Extensions;
using GigHub.Persistence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.IntegrationTest.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;
        private GigHub.Controllers.API.GigsController _controllerApi;

        /// <summary>
        /// This method sets the context and controller
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
            _controllerApi = new GigHub.Controllers.API.GigsController(
                new UnitOfWork(_context));
        }

        /// <summary>
        /// As we created a context in a SetUp method we must dispose it in a TearDown
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        //Add Isolated into this test as we want to rollback all the changes made in this test
        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigsForArtist()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Mine();

            /* To check if data passes to View we need to check the result of method and what data were passed to the view
            * ActionResult shall be casted into ViewResult or change the return type inside controller
            * Model is object we send IEnumerable<Gig> therefore we have to cast it
            */

            //Assert
            ((result as ViewResult).ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenGig()
        {
            //Arrange
            /*
             * Here we first create and instert a user and gig into the database
             */
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            //we have to know what's the Genre's id
            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Update(new GigFormViewModel
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "+",
                GenreId = 2 //here we update the genre
            });

            //Assert
            /*
             * First of All we have to refresh the gig 
             * because it's currently in DbContext
             * and in this controller in this action 
             * we're going to change the database 
             * but the changes will not be reflected in this object in memory
             */
            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("+");
            gig.GenreId.Should().Be(2);
        }

        [Test, Isolated]
        public void Cancel_WhenCalled_ShouldCancelTheGivenGig()
        {
            //Arrange
            /*
             * Here we first create and instert a user and gig into the database
             */
            var user = _context.Users.First();
            _controllerApi.MockCurrentUser(user.Id, user.UserName);

            //we have to know what's the Genre's id
            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig
            {
                Id = 100,
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controllerApi.Cancel(100);

            //Assert
            //_context.Entry(gig).Reload();
            gig.Venue.Should().Be("-");
            gig.GenreId.Should().Be(1);
            gig.Id.Should().Be(100);
            //gig.IsCancelled.Should().Be(true);
        }
    }
}
