
using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    // GET: api/reservations
    // GET: api/reservations?date=2026-12-10&status=confirmed&roomId=1
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetReservations(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var reservations =
            InMemoryData.Reservations.AsEnumerable();

        if (date.HasValue)
            reservations = reservations.Where(r =>
                r.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            reservations = reservations.Where(r =>
                r.Status.Equals(
                    status,
                    StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            reservations = reservations.Where(r =>
                r.RoomId == roomId.Value);

        return Ok(reservations.ToList());
    }

    // GET: api/reservations/1
    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetReservation(
        [FromRoute] int id)
    {
        var reservation = InMemoryData.Reservations
            .FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    // POST: api/reservations
    [HttpPost]
    public ActionResult<Reservation> CreateReservation(
        [FromBody] Reservation reservation)
    {
        var room = InMemoryData.Rooms
            .FirstOrDefault(r =>
                r.Id == reservation.RoomId);

        if (room == null)
            return BadRequest("Room does not exist.");

        if (!room.IsActive)
            return BadRequest("Room is not active.");

        bool conflict = InMemoryData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date);

        if (conflict)
            return Conflict(
                "Room is already reserved on this date.");

        reservation.Id =
            InMemoryData.Reservations.Count == 0
                ? 1
                : InMemoryData.Reservations.Max(r => r.Id) + 1;

        InMemoryData.Reservations.Add(reservation);

        return CreatedAtAction(
            nameof(GetReservation),
            new { id = reservation.Id },
            reservation);
    }

    // PUT: api/reservations/1
    [HttpPut("{id:int}")]
    public ActionResult<Reservation> UpdateReservation(
        [FromRoute] int id,
        [FromBody] Reservation updatedReservation)
    {
        var reservation = InMemoryData.Reservations
            .FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        var room = InMemoryData.Rooms
            .FirstOrDefault(r =>
                r.Id == updatedReservation.RoomId);

        if (room == null)
            return BadRequest("Room does not exist.");

        if (!room.IsActive)
            return BadRequest("Room is not active.");

        bool conflict = InMemoryData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date == updatedReservation.Date);

        if (conflict)
            return Conflict(
                "Room is already reserved on this date.");

        reservation.RoomId = updatedReservation.RoomId;
        reservation.OrganizerName =
            updatedReservation.OrganizerName;
        reservation.Topic = updatedReservation.Topic;
        reservation.Date = updatedReservation.Date;
        reservation.StartTime = updatedReservation.StartTime;
        reservation.EndTime = updatedReservation.EndTime;
        reservation.Status = updatedReservation.Status;

        return Ok(reservation);
    }

    // DELETE: api/reservations/1
    [HttpDelete("{id:int}")]
    public IActionResult DeleteReservation(
        [FromRoute] int id)
    {
        var reservation = InMemoryData.Reservations
            .FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        InMemoryData.Reservations.Remove(reservation);

        return NoContent();
    }
}
