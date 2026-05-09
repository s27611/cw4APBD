using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] DateTime? date, [FromQuery] string? status, [FromQuery] int? roomId)
        {
            var reservations = AppData.Reservations.AsQueryable();

            if (date.HasValue)
                reservations = reservations.Where(r => r.Date.Date == date.Value.Date);

            if (!string.IsNullOrEmpty(status))
                reservations = reservations.Where(r => r.Status == status);

            if (roomId.HasValue)
                reservations = reservations.Where(r => r.RoomId == roomId);

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null) return BadRequest("Sala nie istnieje");
            if (!room.IsActive) return BadRequest("Sala nieaktywna");

            bool conflict = AppData.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date.Date == reservation.Date.Date &&
                reservation.StartTime < r.EndTime &&
                reservation.EndTime > r.StartTime
            );

            if (conflict)
                return Conflict("Konflikt rezerwacji");

            reservation.Id = AppData.Reservations.Max(r => r.Id) + 1;
            AppData.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservation updated)
        {
            var existing = AppData.Reservations.FirstOrDefault(r => r.Id == id);
            if (existing == null) return NotFound();

            updated.Id = id;
            AppData.Reservations.Remove(existing);
            AppData.Reservations.Add(updated);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();

            AppData.Reservations.Remove(reservation);
            return NoContent();
        }
    }
}