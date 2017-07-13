using System.Linq;
using System.Web.Http;
using GigHub.Core.DTOs;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto) //[FromBody] int gigId) // thanks to DTOs
        {
            if (_unitOfWork.Attendance
                .GetGigUserAttendance(User.Identity.GetUserId(), dto.GigId) != null)
                return BadRequest("There is already a registered attendace for this gig and current user");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId()
            };

            _unitOfWork.Attendance.Add(attendance);
            _unitOfWork.Complete();

            return Ok(dto.GigId);
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(AttendanceDto dto)
        {
            var attendance = _unitOfWork.Attendance.GetGigUserAttendance(User.Identity.GetUserId(), dto.GigId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendance.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(dto.GigId);
        }
    }
}