using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public Gig Gig { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsGoing { get; set; }

    }
}