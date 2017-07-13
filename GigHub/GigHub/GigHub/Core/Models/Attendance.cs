using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Attendance
    {
        public ApplicationUser Attendee { get; set; }
        public Gig Gig { get; set; }

        [Key]
        [Column(Order = 1)] //this is needed for creating keys for composite keys
        public string AttendeeId { get; set; }

        [Key]
        [Column(Order = 2)] //this is needed for creating keys
        public int GigId { get; set; }
    }
}