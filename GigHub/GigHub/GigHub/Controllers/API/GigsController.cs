using System;
using System.Collections.Generic;
using System.Data.Entity; //for including data into an entity by using lambda expression
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Models.Enums;
using GigHub.Persistence;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            //find Gig with a given id
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            //if the gig is canceled it'll return not found
            if (gig == null || gig.IsCancelled)
                return NotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            /*
             * Due to cohesion things which are highly related shall be kept together
             * there're two cohesions on 
             * architecture level and method level
             */
             //therefore all code for cancelling a gig has been moved to the Gig model respectivly
            gig.Cancel();

            _unitOfWork.Complete();

            return
                Ok(
                    $"The Gig advertised by {gig.Artist.Name} on " +
                    $"{gig.DateTime.Date.ToString("d MMM yyyy")} has been cancelled");
        }

        [HttpPut]
        public IHttpActionResult Update(int gigId)
        {
            //Not Implemented yet
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigById(gigId);
            if (gig == null)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            return Ok();
        }
    }
}
