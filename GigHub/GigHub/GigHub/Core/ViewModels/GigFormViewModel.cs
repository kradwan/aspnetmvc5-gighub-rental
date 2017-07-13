using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using GigHub.Controllers;

namespace GigHub.Core.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Action
        {
            get
            {
                Expression<Func<GigsController, ActionResult>> update = (u => u.Update(this));

                Expression <Func <GigsController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
                //return (Id != 0) ? "Update" : "Create"; instead of this line? wtf
            }
        }

        public DateTime GetDatetime() { return DateTime.Parse($"{Date} {Time}"); }
    }
}