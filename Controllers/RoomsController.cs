
using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    // GET: api/rooms
    // GET: api/rooms?minCapacity=20&hasProjector=true&activeOnly=true
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetRooms(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = InMemoryData.Rooms.AsEnumerable();

        if (minCapacity.HasValue)
            rooms = rooms.Where(r =>
                r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r =>
                r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    // GET: api/rooms/2
    [HttpGet("{id:int}")]
    public ActionResult<Room> GetRoom(
        [FromRoute] int id)
    {
        var room = InMemoryData.Rooms
            .FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        return Ok(room);
    }

    // GET: api/rooms/building/A
    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuilding(
        [FromRoute] string buildingCode)
    {
        var rooms = InMemoryData.Rooms
            .Where(r => r.BuildingCode.Equals(
                buildingCode,
                StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(rooms);
    }

    // POST: api/rooms
    [HttpPost]
    public ActionResult<Room> CreateRoom(
        [FromBody] Room room)
    {
        room.Id = InMemoryData.Rooms.Count == 0
            ? 1
            : InMemoryData.Rooms.Max(r => r.Id) + 1;

        InMemoryData.Rooms.Add(room);

        return CreatedAtAction(
            nameof(GetRoom),
            new { id = room.Id },
            room);
    }

    // PUT: api/rooms/2
    [HttpPut("{id:int}")]
    public ActionResult<Room> UpdateRoom(
        [FromRoute] int id,
        [FromBody] Room updatedRoom)
    {
        var room = InMemoryData.Rooms
            .FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        room.Name = updatedRoom.Name;
        room.BuildingCode = updatedRoom.BuildingCode;
        room.Floor = updatedRoom.Floor;
        room.Capacity = updatedRoom.Capacity;
        room.HasProjector = updatedRoom.HasProjector;
        room.IsActive = updatedRoom.IsActive;

        return Ok(room);
    }

    // DELETE: api/rooms/2
    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(
        [FromRoute] int id)
    {
        var room = InMemoryData.Rooms
            .FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        bool hasReservations = InMemoryData.Reservations
            .Any(r => r.RoomId == id);

        if (hasReservations)
            return Conflict(
                "Room has existing reservations.");

        InMemoryData.Rooms.Remove(room);

        return NoContent();
    }
}
