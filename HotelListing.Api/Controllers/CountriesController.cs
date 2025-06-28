using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;
using HotelListing.Api.Data.Models;
using HotelListing.Api.Data.DTO;
using AutoMapper;
using HotelListing.Api.Contracts;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
       
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepo;
        public CountriesController(IMapper mapper, ICountriesRepository countryRepo)
        {
           this._countriesRepo = countryRepo;
            this._mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
            var countryList = await _countriesRepo.GetAllAsync();
            var resultSet = _mapper.Map<List<GetCountryDTO>>(countryList);
            return resultSet;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            var country = await _countriesRepo.GetDetails(id);
                //.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.CountryId == id);

            if (country == null)
            {
                return NotFound();
            }
            var countryResult = _mapper.Map<CountryDTO>(country);
            return countryResult;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountry)
        {
            //var country = await _countriesRepo.GetAsync(id);
            if (await _countriesRepo.Exists(id) != true)
            { return NotFound(); }
            
            var country = await _countriesRepo.GetAsync(id);
            _mapper.Map(updateCountry, country);
            //country.Name = updateCountry.Name;
            //country.ShortName = updateCountry.ShortName;

            try
            {
                await _countriesRepo.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountry countryDTO)
        {
            var country = _mapper.Map<Country>(countryDTO);
            await _countriesRepo.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //var country = await _context.Countries.FindAsync(id);
            //if (country == null)
            //{
            //    return NotFound();
            //}
            if (await _countriesRepo.Exists(id) != true)
            { return NotFound(); }
            await _countriesRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepo.Exists(id);
        }
    }
}
