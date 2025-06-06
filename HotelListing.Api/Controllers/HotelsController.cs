using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private static List<Hotel> List = new List<Hotel>
    {
        new Hotel { Id = 1, Name = "Grand Plaza", Address ="123 Main St", Rating = 3.5 },
        new Hotel { Id = 2, Name = "Ocean View", Address ="456 Beach Rd", Rating = 4.5 }
    };

    // GET: api/<HotelsController>
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(List);
    }

    // GET api/<HotelsController>/5
    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        Hotel hotel = List.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return Ok(hotel);
    }

    // POST api/<HotelsController>
    [HttpPost]
    public ActionResult<Hotel> Post([FromBody]Hotel hotel)
    //[FormBody] maps the body of the HTTP request (JSON) into the object of Hotel ,i.e., hotel (Model Binding)
    {
        if (List.Any(h => h.Id == hotel.Id)) //if list contains 'h' where h.Id is equal to hotel.Id
            return BadRequest("Hotel with this id already exist");
        List.Add(hotel);
        return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
        // nameof(GET) redirects/routes the request to the Get(int id) method above
        // new { id = hotel.Id } passes the id in the Get(int id)
        // hotel - Includes the new hotel object in the response body

    }

    // PUT api/<HotelsController>/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
    {
        Hotel existingHotel = List.FirstOrDefault(h => h.Id == id);
        if (existingHotel == null)
        {
            return NotFound();
        }
        existingHotel.Address = updatedHotel.Address;
        existingHotel.Id = updatedHotel.Id;
        existingHotel.Name = updatedHotel.Name;
        existingHotel.Rating = updatedHotel.Rating;
        return NoContent();
    }

    // DELETE api/<HotelsController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        Hotel existingHotel = List.FirstOrDefault(h => h.Id == id);
        if (existingHotel == null)
        {
            return NotFound(new { message = "Hotel not found" }); 
        }
        List.Remove(existingHotel);
        return NoContent();
    }
}
