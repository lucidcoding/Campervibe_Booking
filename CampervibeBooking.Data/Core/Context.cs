using CampervibeBooking.Domain.Entities;
using System.Configuration;
using System.Data.Entity;

namespace CampervibeBooking.Data.Core
{
    public class Context : DbContext
    {
        public Context() : base(ConfigurationManager.ConnectionStrings["CampervibeBooking"].ConnectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().ToTable("Booking");

           
            base.OnModelCreating(modelBuilder);
        }
    }
}
