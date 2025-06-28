using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _dbContext;

        public CountriesRepository(HotelListingDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _dbContext.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.CountryId == id);
        }
    }
}
