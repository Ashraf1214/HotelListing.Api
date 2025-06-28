using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Dependencey injection , contructor injection for dbcontext
        private readonly HotelListingDbContext _dbcontext;
        public GenericRepository(HotelListingDbContext dbContext)
        {
            this._dbcontext = dbContext;
        }
        //async programming T = any generic class and entity is actual object of that class
        //Expects a return type of that class
        public async Task<T> AddAsync(T entity)
        {
            //adds entity/object of country in the database
            await _dbcontext.Set<T>().AddAsync(entity);
            //Saves the insertion
            await _dbcontext.SaveChangesAsync();
            //returns that country object
            return entity;
        }
        //no retrun type since it's Task instead of Task<T>
        public async Task DeleteAsync(int id)
        {
            //Stores the country object of matching id in 'entity' variable
            var entity = await GetAsync(id);
            //deletes that object that maps to the entry/row in the database
            _dbcontext.Set<T>().Remove(entity);
            //Saves after deleting
            await _dbcontext.SaveChangesAsync();   
        }

        public async Task<bool> Exists(int id)
        {
            //Stores the country object of matching id in 'entity' variable
            var entity =  await GetAsync(id);
            //Checks if the object has any content
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            //returns a list of the objects/countries from database
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            //Checks if the id exists
            if (id is null)
            {
                return null;
            }
            //returns the country object with matching ID
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            //Updates the table with the entity/country object
            _dbcontext.Update(entity);
            //saves after update
            await _dbcontext.SaveChangesAsync();
        }
    }
}
