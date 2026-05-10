using ExampleApp.DTOs;

namespace ExampleApp.Services;

public interface IRoomService
{
    IEnumerable<RoomDto> GetAll(int? minCapacity, bool? hasProjector, bool? activeOnly);
    RoomDto GetById(int id);
    IEnumerable<RoomDto> GetByBuilding(string buildingCode);
    RoomDto Add(CreateRoomDto room);
    RoomDto Update(int id, UpdateRoomDto room);
    void Remove(int id);
}