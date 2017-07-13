using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Controllers.API;
using GigHub.Core;
using Moq;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Management;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;

// reference to the controller

namespace GigHub.Tests.Controllers.Api
{
    /// <summary>
    /// Summary description for GigControllerTests
    /// </summary>
    [TestClass]
    public class GigControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitilize()//public GigControllerTests()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>(); // 2.2 This is a generic class; add ref: Moq, Core
            mockUnitOfWork.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);
                //whit this when we access the Mock's Gigs then we initialize IGigRepository

            //referencing controller from our project, therefore we need an object passed to this controller
            _controller = new GigsController(mockUnitOfWork.Object);
            // this mock.Object is an actual implementation of IUnitOfWork
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com"); //this is an extension method
        }

        /*
         * Added by a framework 
         *
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        */
        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        //Roy Osherove's naming convention, that he introduced in Art of Unit tests book
        /*
         * Cancel - method of GigsController
         * _NoGigWithGivenIdExists - condition we're testing
         * _ShouldReturnNotFound - expected result, what you think should happen when we call this action, the action should return not found message, standard RESTFull convention
         */

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            //because we've not used a setup method in our mockRepository
            //if we call _unitOfWork.Gigs.GetGigWithAttendees(id)
            //it will return null by default
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>(); //Should() is a FluentAssertion from the installed package
        }

        /// <summary>
        /// I expect when I call it at a given condition it should behave as I expect
        /// </summary>
        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            //here we mock a gig by ourselves
            var gig = new Gig();
            gig.Cancel(); //set a cancel gig

            //here we setup that GetGigWithAttendees(1) inside _controller.Cancel(1) will
            //return this gig above
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig()
            { ArtistId = _userId + "-" };// we need to use a different user, instead of using Magic numbers, better cleaner more maintainable approach

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
