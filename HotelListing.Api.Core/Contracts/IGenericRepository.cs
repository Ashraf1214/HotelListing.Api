using HotelListing.Api.Data.Pagination;

namespace HotelListing.Api.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);

        Task<PagedResults<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
    }
}
