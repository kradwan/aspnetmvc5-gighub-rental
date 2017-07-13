using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.DTOs
{
    /// <summary>
    /// DTO - Data transfer Object
    /// it's an architectual pattern to send data accross processes
    /// so here we have a process which is running on a client and on a server, so we create DTO to communicate between these processes
    /// </summary>
    public class AttendanceDto
    {
        public int GigId { get; set; }
    }
}