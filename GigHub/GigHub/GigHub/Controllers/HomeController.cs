using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; //added to allow better approach to include Artists to Genres inside index
using System.Web;
using System.Web.Mvc;
using GigHub.Persistence.Repositories;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        //THIS is an example to compare how I decoupled the GigsController and rest 
        //than the old aproach which is highly coupled to Repositories and ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private readonly GigRepository _gigRepository;
        private readonly AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _gigRepository = new GigRepository(_context);
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _gigRepository.GetUpcomingGigs();
                
            if (!string.IsNullOrEmpty(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                         g.Genre.Name.Contains(query) ||
                                         g.Venue.Contains(query));
            }

            var userId = User.Identity.GetUserId();

            var attendances = _attendanceRepository.GetUserAttendances(userId)
                //ToLookup() is like a dictionary, so internaly uses hash table to quickly lookup tables, here we lookup using GigId
                .ToLookup(a => a.GigId); 

            var homeViewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };
            return View("Gigs", homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}