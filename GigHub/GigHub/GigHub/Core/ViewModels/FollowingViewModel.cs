using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class FollowingViewModel
    {
        public IEnumerable<ApplicationUser> Followees { get; set; }

        public IEnumerable<ApplicationUser> Followers { get; set; }

        public bool ShowActions { get; set; }

    }
}