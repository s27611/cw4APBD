using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class AppData
    {
        public static List<Room> Rooms = new List<Room>
        {
            new Room { Id = 1, Name = "A101", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "B202", BuildingCode = "B", Floor = 2, Capacity = 20, HasProjector = false, IsActive = true },
            new Room { Id = 3, Name = "C303", BuildingCode = "C", Floor = 3, Capacity = 50, HasProjector = true, IsActive = false },
            new Room { Id = 4, Name = "A102", BuildingCode = "A", Floor = 1, Capacity = 15, HasProjector = true, IsActive = true },
            new Room { Id = 5, Name = "B201", BuildingCode = "B", Floor = 2, Capacity = 40, HasProjector = false, IsActive = true }
        };

        public static List<Reservation> Reservations = new List<Reservation>
        {
            new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Kowalski", Topic = "C# Basics", Date = DateTime.Today, StartTime = new TimeSpan(9,0,0), EndTime = new TimeSpan(11,0,0), Status = "confirmed" },
            new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Nowak", Topic = "REST API", Date = DateTime.Today, StartTime = new TimeSpan(12,0,0), EndTime = new TimeSpan(14,0,0), Status = "planned" },
            new Reservation { Id = 3, RoomId = 1, OrganizerName = "Piotr Wiśniewski", Topic = "Docker", Date = DateTime.Today.AddDays(1), StartTime = new TimeSpan(10,0,0), EndTime = new TimeSpan(12,0,0), Status = "confirmed" }
        };
    }
}