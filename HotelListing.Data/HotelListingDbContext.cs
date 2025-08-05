using HotelListing.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

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

    //    public class HotelListingDbContextFactory : IDesignTimeDbContextFactory<HotelListingDbContext>
    //    {
    //        public HotelListingDbContext CreateDbContext(string[] args)
    //        {
    //            IConfiguration config = new ConfigurationBuilder()
    //                .SetBasePath(Directory.GetCurrentDirectory())
    //                .AddJsonFile("appsettings.json", Optional: false, realoadOnChange: true)
    //                .Build();

    //            var optionsBuilder = new DbContextOptionsBuilder<HotelListingDbContext>();
    //            var conn = config.GetConnectionString("HotelListingConnectionString");
    //            optionsBuilder.UseSqlServer(conn);
    //            return new HotelListingDbContext(optionsBuilder.Options);

    //        }
    //    }
}
