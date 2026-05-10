using ExampleApp.Models;

namespace ExampleApp.Repositories;

public interface IRoomRepository
{
    IEnumerable<Room> GetAll(); 
    IEnumerable<Room> GetByBuilding(string buildingCode);
    Room? GetById(int id);
    void Add(Room room);
    bool Update(Room room);
    void Remove(Room room);
    bool Exists(int id);
}