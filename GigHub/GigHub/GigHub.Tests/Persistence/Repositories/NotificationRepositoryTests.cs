using System;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private UserNotificationRepository _userNotificationRepository;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;

        [TestInitialize]
        public void InitializeNotificationRepository()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(r => r.UserNotifications).Returns(_mockUserNotifications.Object);

            _userNotificationRepository = new UserNotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetNewUserNotifications_NotificationHasBeenRead_ShouldNotBeReturn()
        {
            var userNotification = new UserNotification(new ApplicationUser() { Id = "1" }, new Notification()) { IsRead = true };
            _mockUserNotifications.SetSource(new[] { userNotification });

            var result = _userNotificationRepository.GetNewUserNotifications("1");
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewUserNotifications_NotificationIsForDifferentArtist_ShouldNotBeReturned()
        {
            var userNotification = new UserNotification(new ApplicationUser() { Id = "1" }, new Notification());
            _mockUserNotifications.SetSource(new[] { userNotification });

            var result = _userNotificationRepository.GetNewUserNotifications("1"+"-");
            result.Should().BeEmpty();
        }
    }
}