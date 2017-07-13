using System;
using GigHub.Core.Models.Enums;

namespace GigHub.Core.DTOs
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        public GigDto Gig { get; set; }
    }
}