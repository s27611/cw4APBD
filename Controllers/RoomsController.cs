using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var rooms = AppData.Rooms.AsQueryable();

            if (minCapacity.HasValue)
                rooms = rooms.Where(r => r.Capacity >= minCapacity);

            if (hasProjector.HasValue)
                rooms = rooms.Where(r => r.HasProjector == hasProjector);

            if (activeOnly == true)
                rooms = rooms.Where(r => r.IsActive);

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetByBuilding(string buildingCode)
        {
            var rooms = AppData.Rooms.Where(r => r.BuildingCode == buildingCode);
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            room.Id = AppData.Rooms.Max(r => r.Id) + 1;
            AppData.Rooms.Add(room);

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Room updatedRoom)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            updatedRoom.Id = id;
            AppData.Rooms.Remove(room);
            AppData.Rooms.Add(updatedRoom);

            return Ok(updatedRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            if (AppData.Reservations.Any(r => r.RoomId == id))
                return Conflict("Sala ma rezerwacje");

            AppData.Rooms.Remove(room);
            return NoContent();
        }
    }
}