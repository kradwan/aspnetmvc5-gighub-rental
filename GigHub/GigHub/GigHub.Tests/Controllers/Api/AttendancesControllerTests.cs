using System;
using System.Net;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.API;
using GigHub.Core;
using GigHub.Core.DTOs;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;
        [TestInitialize]
        public void TestInitilize()//public GigControllerTests()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Attendance).Returns(_mockRepository.Object);

            //referencing controller from our project, therefore we need an object passed to this controller
            _controller = new AttendancesController(mockUnitOfWork.Object);
            // this mock.Object is an actual implementation of IUnitOfWork
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com"); //this is an extension method
        }

        [TestMethod]
        public void Attend_WhenUserAttendanceExistForGig_ShouldReturnBadRequest()
        {
            var attendance = new Attendance()
            {
                Attendee = new ApplicationUser() {Id = _userId},
                GigId = 1
            };

            _mockRepository.Setup(r => r.GetGigUserAttendance("1", 1)).Returns(attendance);

            var result =_controller.Attend(new AttendanceDto() {GigId = 1});
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var gigId = 1;
            var result = _controller.Attend(new AttendanceDto() { GigId = gigId }) ;
            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
            var msg = _controller.Attend(new AttendanceDto() { GigId = 1 }) as OkNegotiatedContentResult<int>;
            msg.Content.Should().Be(gigId);

        }
    }
}
