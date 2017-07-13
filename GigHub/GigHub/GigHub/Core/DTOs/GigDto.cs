using System;
using GigHub.Core.Models;

namespace GigHub.Core.DTOs
{
    public class GigDto
    {
        public int Id { get; set; }

        public UserDto Artist { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public bool IsCancelled { get; set; }
    }
}