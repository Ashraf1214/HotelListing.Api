using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;
using HotelListing.Api.Data.Models;
using HotelListing.Api.Contracts;
using AutoMapper;
using HotelListing.Api.Data.DTO.Hotel;
using Microsoft.AspNetCore.Authorization;
using HotelListing.Api.Data.Pagination;

namespace HotelListing.Api.Controllers
{
    [Route("api/v{version:apiVersion}/Hotels")]
    [ApiController]
    [ApiVersion("2.0")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository hotelRepo, IMapper mapper)
        {
            _hotelRepo = hotelRepo;
            this._mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        //[Authorize(Roles = "Administrator,User")]
        public async Task<ActionResult<IEnumerable<GetAllHotelsDTO>>> GetHotels()
        {
            var hotels = await _hotelRepo.GetAllAsync();
            return _mapper.Map<List<GetAllHotelsDTO>>(hotels);
        }

        [HttpGet("Paged")]
        //[Authorize(Roles = "Administrator,User")]
        public async Task<ActionResult<PagedResults<GetAllHotelsDTO>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            var pagedHotels = await _hotelRepo.GetAllAsync<GetAllHotelsDTO>(queryParameters);
            return Ok(pagedHotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        //Authorize(Roles = "Administrator,User")]
        public async Task<ActionResult<GetHotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotelRepo.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetHotelDTO>(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDTO hotel)
        {
            if (!await _hotelRepo.Exists(id))
            {
                return BadRequest();
            }
            var existingHotel = await _hotelRepo.GetAsync(id);
            _mapper.Map(hotel, existingHotel);
            try
            {
                await _hotelRepo.UpdateAsync(existingHotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO hotelDTO)
        {
            
            var hotel = _mapper.Map<Hotel>(hotelDTO);
            await _hotelRepo.AddAsync(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepo.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepo.Exists(id);
        }
    }
}
