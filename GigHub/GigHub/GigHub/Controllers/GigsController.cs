using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        //I put here a readonly as it will be used only in a constructor and not re-initialize anywhere else
        //private readonly ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;

        /* Dependency Inversion 
         * 
         * - sending IUnitOfWork inside the constructor method parameter
         * 
         * Principles:
         * 
         * 1. Low level aplication logic should not depend on high level therefore I implemented IUnitOfWork to decouple 
         * from Controller <=> UnitOfWork
         * to Controller <=> IUnitOfWork <=> UnitOfWork
         * therefore in case of any change made in UnitOfWork the controller is not needed to be recompiled or redeployed
         * 
         * 2. Abstraction should not relay on details, details should relay on abstraction
         * 
         * It means ex.:
         * IUnitOfWork relaied on GigRepository - violation
         * IUnitOfWork shall relay only on IGigRepository
         * 
         * I send IUnitOfWork in the constructor method parameter instead of initializating AplicationDbContext as it's
         * tightly coupling to EntityFramework, we get rid of this coupling by providing IUnitOfWork
         * By Dependency Injection - we inject this IUnityOfWork via it's constructor
         * 
         * to make it running we have to bring and apply DependencyInjection Framework like: Unity, StructureMap, Autofac, Ninject
         * 
         * Ninject has been chosen due to it's easy way to configure Dependancy Injection
         * STEPS:
         * 1. add Ninject from nuget package: 
         * PM> Install-Package Ninject.Mvc5 -Version:3.2.1.0
         * 
         * 2. add Ninject for WebApi from nuget - this supports dependancy injection for WebApi
         * PM> Install-Package Ninject.Web.WebApi -Version:3.2.1.0
         * 
         * static class NinjectWebCommon has been added
         * 
         * poor way of adding references to dependencies
         * inside private static void RegisterServices(IKernel kernel)
         *   kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
         * 
         * therefore it;s better to use Convention Over Configuration
         * 3. Install 
         * 
        */
        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Title = "Create a Gig",
                Heading = "Create a Gig",
                Genres = _unitOfWork.Genres.GetGenres()
            };
            return View("GigForm", viewModel);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigById(id);

            if (gig == null)
                return HttpNotFound($"There is no a gig with id= {id}");

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult($"You have no access to update the gig!");

            var viewModel = new GigFormViewModel
            {
                Title = "Edit a Gig",
                Heading = "Edit a Gig",

                Id = gig.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
                GenreId = gig.GenreId,
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm")
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var followers = _unitOfWork.Following.GetUserFolowers(userId);

            var gig = new Gig(userId, viewModel, followers);

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return new HttpNotFoundResult();
            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDatetime(), viewModel.Venue, viewModel.GenreId);

            gig.GenreId = viewModel.GenreId;
            gig.DateTime = viewModel.GetDatetime();
            gig.Venue = viewModel.Venue;

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Mine()
        {
            return View(_unitOfWork
                .Gigs
                .GetUpcomingGigsByArtist(User.Identity.GetUserId()));
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            GigsViewModel gigsAttending = new GigsViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm attending",
                Attendances = _unitOfWork.Attendance.GetFutureUserAttendances(userId).ToLookup(a => a.GigId)
            };


            return View("Gigs", gigsAttending);
        }



        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followees = new FollowingViewModel()
            {
                Followees = _unitOfWork.Following.GetUserFollowees(userId),
                ShowActions = User.Identity.IsAuthenticated
            };
            return View("Following", followees);
        }

        [HttpPost]//can be called only from the Form-control
        public ActionResult Search(GigsViewModel gigsViewModel)
        {
            return RedirectToAction("Index", "Home", new { query = gigsViewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            if (id < 1)
                return HttpNotFound("Incorrect gig id has been delivered");

            var gig = _unitOfWork.Gigs.GetGigWithArtistGenreById(id);

            if (gig == null)
                return HttpNotFound();

            var gigViewModel = new GigDetailsViewModel()
            {
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Details",
                Gig = gig
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                gigViewModel.IsFollowing = _unitOfWork.Following.FollowingArtistByUser(gig.ArtistId, userId);
                gigViewModel.IsGoing = _unitOfWork.Attendance.AttendigGigByUser(id, userId);

            }

            return View("Details", gigViewModel);
        }

    }
}