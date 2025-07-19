using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Data.Models;

namespace HotelListing.Api.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
