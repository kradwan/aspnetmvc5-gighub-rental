using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    //this class is for extracting dependancies which are in Fluent API inside ApplicationDBContext and are highly related to EntityFramework
    //therefore this class is just for configuring Validation on Gig class, the annotation attributes have been moved here
    //Here we can find the whole configuration for a Gig class
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            Property(g => g.GenreId)
                .IsRequired();

            Property(g => g.ArtistId)
                .IsRequired();

            Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);

            //To configure relationship like below, put everything into the parent which in this case is Gig as it's required
            //modelBuilder
            //    .Entity<Attendance>()
            //    .HasRequired(g => g.Gig)
            //    .WithMany(a => a.Attendees)
            //    .WillCascadeOnDelete(false);
            HasMany(g => g.Attendees)
                .WithRequired(a=> a.Gig)
                .WillCascadeOnDelete(false);
        }
    }
}