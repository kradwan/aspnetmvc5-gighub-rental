using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GigHub.Tests.Domain
{
    [TestClass]
    public class GigTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var gig = new Gig();
            gig.Cancel();
            gig.IsCancelled.Should().BeTrue();
        }


        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveANotification()
        {
            var gig = new Gig();
            gig.Attendees.Add(new Attendance() {Attendee = new ApplicationUser() {Id = "1"} });

            gig.Cancel();

            var attendees = gig.GetAttendees();
            attendees[0].UserNotifications.Count.Should().Be(1);
        }
    }
}
