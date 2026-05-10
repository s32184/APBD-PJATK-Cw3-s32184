using ExampleApp.Models;

namespace ExampleApp.Repositories;

public class RoomRepository : IRoomRepository
{
    private static int _nextId = 1;
    private readonly List<Room> _rooms = [];

    public RoomRepository()
    {
        _rooms.AddRange(new[]
        {
            new Room
            {
                Id = _nextId++,
                Name = "Sala Konferencyjna 1",
                BuildingCode = "D",
                Floor = 1,
                Capacity = 50,
                HasProjector = true,
                IsActive = true
            },
            new Room
            {
                Id = _nextId++,
                Name = "Laboratorium Sieciowe",
                BuildingCode = "B",
                Floor = 2,
                Capacity = 18,
                HasProjector = false,
                IsActive = true
            },
            new Room
            {
                Id = _nextId++,
                Name = "Sala Konferencyjna 2",
                BuildingCode = "D",
                Floor = 2,
                Capacity = 25,
                HasProjector = true,
                IsActive = false
            },
            new Room
            {
                Id = _nextId++,
                Name = "Laboratorium Sieciowe 2",
                BuildingCode = "B",
                Floor = 1,
                Capacity = 23,
                HasProjector = false,
                IsActive = false
            }
        });
    }
    
    public IEnumerable<Room> GetAll()
    {
        return _rooms;
    }

    public Room? GetById(int id)
    {
        return _rooms.FirstOrDefault(x => x.Id == id);
    }
    
    public IEnumerable<Room> GetByBuilding(string buildingCode)
    {
        return _rooms.Where(x =>
            x.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase));
    }

    public void Add(Room room)
    {
        room.Id = _nextId++;
        _rooms.Add(room);
    }

    public bool Update(Room room)
    {
        var existing = GetById(room.Id);
        if (existing is null)
        {
            return false;
        }
        
        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;
        return true;
    }

    public void Remove(Room room)
    {
        _rooms.Remove(room);
    }

    public bool Exists(int id)
    {
        return _rooms.Any(x => x.Id == id);
    }
}