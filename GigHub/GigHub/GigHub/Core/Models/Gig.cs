using GigHub.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public bool IsCancelled { get; private set; }

        //added relationship between Gig and Attandace therefore I shall modify fluent API in AppDBContext
        public ICollection<Attendance> Attendees { get; private set; }

        public Gig()
        {
            Attendees = new Collection<Attendance>();
        }

        public Gig(string userId, GigFormViewModel viewModel, ICollection<Following> followers)
        {
            Create(userId, viewModel, followers);
        }

        public void Modify(DateTime dateTime, string venue, byte genreId)
        {
            //call the factory static method to create a proper object
            var notification = Notification.GigUpdated(this, DateTime, Venue); //use existing values for this Gig

            DateTime = dateTime;
            Venue = venue;
            GenreId = genreId;

            foreach (var attendee in Attendees.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        //Method to cancel the gig moved from the controller as it's not his responsibility
        public void Cancel()
        {
            IsCancelled = true;

            foreach (var attendee in Attendees.Select(a => a.Attendee))
            {
                attendee.Notify(Notification.GigCanceled(this));
            }
        }

        public Gig Create(string userId, GigFormViewModel viewModel, ICollection<Following> followers)
        {
            ArtistId = userId;
            DateTime = viewModel.GetDatetime();//DateTime.Parse($"{viewModel.Date} {viewModel.Time}"), --> Information Expert principle, this code was moved to the class which has these two information Date and Time which it parse
            GenreId = viewModel.GenreId;
            Venue = viewModel.Venue;

            foreach (var follower in followers.Select(f => f.Follower))
            {
                follower.Notify(Notification.GigCreated(this));
            }

            return this;
        }

        public List<ApplicationUser> GetAttendees()
        {
            return Attendees.Select(a => a.Attendee).ToList();
        }
    }
}