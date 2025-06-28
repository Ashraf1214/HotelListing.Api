using HotelListing.Api.Data.Models;

namespace HotelListing.Api.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
