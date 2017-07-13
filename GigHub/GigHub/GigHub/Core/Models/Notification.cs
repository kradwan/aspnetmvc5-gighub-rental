using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GigHub.Core.Models.Enums;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }

        //These props got private setters as they shall not be modyfied by clients
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]//to make not nullable
        public Gig Gig { get; private set; }

        public Notification()
        {
        }

        //Pattern 1.1 
        //set constructor accessor to private, because I don't want to use this constructor anywhere else than here as it can create an object in an Invalid State
        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("GigNotification");

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }

        /* Pattern 1
         * Using Simple Factory Methods to Create Objects
         * 
         * Factory method is responsible for creating an object in a Valid State
         */

        //Pattern 1.2
        //create static factory methods
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notifcation = new Notification(NotificationType.GigUpdated, newGig);
            notifcation.OriginalDateTime = originalDateTime;
            notifcation.OriginalVenue = originalVenue;
            return notifcation;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}