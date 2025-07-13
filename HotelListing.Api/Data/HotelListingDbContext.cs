using HotelListing.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HotelListing.Api.Data
{
    public class HotelListingDbContext : IdentityDbContext<Apiuser>
    {
        public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        //
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfiguration(new Configuration.RoleConfigurations());
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.ConfigureWarnings(warnings =>
        //        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        //}
    }
}
