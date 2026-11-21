
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Data;

public static class InMemoryData
{
    public static List<Room> Rooms = new()
    {
        new Room
        {
            Id = 1,
            Name = "Lab 101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Lab 204",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Conference Room",
            BuildingCode = "A",
            Floor = 2,
            Capacity = 40,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Workshop Room",
            BuildingCode = "C",
            Floor = 1,
            Capacity = 15,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 5,
            Name = "Old Lab",
            BuildingCode = "B",
            Floor = 3,
            Capacity = 10,
            HasProjector = false,
            IsActive = false
        }
    };

    public static List<Reservation> Reservations = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "C# Workshop",
            Date = new DateOnly(2026, 12, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Jan Nowak",
            Topic = "REST API",
            Date = new DateOnly(2026, 12, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "scheduled"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Maria Smith",
            Topic = "Cybersecurity",
            Date = new DateOnly(2026, 12, 11),
            StartTime = new TimeOnly(12, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 4,
            OrganizerName = "Tom Brown",
            Topic = "Networking",
            Date = new DateOnly(2026, 12, 12),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "scheduled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 2,
            OrganizerName = "Eva Green",
            Topic = "Cloud Computing",
            Date = new DateOnly(2026, 12, 13),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(16, 0),
            Status = "confirmed"
        }
    };
}
