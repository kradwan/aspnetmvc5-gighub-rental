using System.Linq;
using System.Web.Http;
using GigHub.Core.DTOs;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    public class FollowingController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FolloweeDto dto)
        {
            if (_unitOfWork.Following.FollowingArtistByUser(dto.FolloweeId, User.Identity.GetUserId()))
                return Json(new string[] {"There is a following for this artist already added", 301.ToString()});

            var following = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = User.Identity.GetUserId()
            };
            _unitOfWork.Following.Add(following);
            _unitOfWork.Complete();

            return Ok($"Now you're following ${User.Identity.Name}");
        }

        [HttpPost]
        public IHttpActionResult UnFollow(FolloweeDto dto)
        {
            var following = _unitOfWork.Following.GetArtistFollowingByUser(dto.FolloweeId, User.Identity.GetUserId());

            if (following != null)
            {
                foreach (var f in following)
                {
                    _unitOfWork.Following.Remove(f);
                }
                _unitOfWork.Complete();

                return Ok($"Now you're not following ${User.Identity.Name} anymore");
            }

            return BadRequest("There is no following to be removed for this gig");
        }
    }
}
