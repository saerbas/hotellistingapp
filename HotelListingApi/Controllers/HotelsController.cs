using HotelListingApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    public static List<Hotel?> hotels = new List<Hotel?>
    {
        new Hotel
        {
            Id = 1,
            Name = "Hotel 1",
            Address = "123 Main St",
            Rating = "PG"
        },
        new Hotel
        {
            Id = 2,
            Name = "Hotel 2",
            Address = "456 Main St",
            Rating = "PG"
        }
    };
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(hotels);
    }

    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return Ok(hotel);
    }

    [HttpPost]
    public ActionResult<Hotel> Post([FromBody] Hotel hotel)
    {
        if (hotels.Any(h => h.Id == hotel.Id))
        {
            return BadRequest();
        }
        hotels.Add(hotel);
        return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
    }

    [HttpPut("{id}")]
    public ActionResult<Hotel> Put(int id, [FromBody] Hotel hotel)
    {
        var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
        if (existingHotel == null)
        {
            return NotFound();
        }
        existingHotel.Name = hotel.Name;
        existingHotel.Address = hotel.Address;
        existingHotel.Rating = hotel.Rating;
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        hotels.Remove(hotel);
        return NoContent();
    }
}