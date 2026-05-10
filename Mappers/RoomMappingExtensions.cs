using ExampleApp.DTOs;
using ExampleApp.Models;

namespace ExampleApp.Mappers;

public static class RoomMappingExtensions
{
    public static Room ToDomain(this CreateRoomDto dto)
    {
        return new Room
        {
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Floor = dto.Floor,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = dto.IsActive
        };
    }

    public static void MapTo(this UpdateRoomDto dto, Room room)
    {
        room.Name = dto.Name;
        room.BuildingCode = dto.BuildingCode;
        room.Floor = dto.Floor;
        room.Capacity = dto.Capacity;
        room.HasProjector = dto.HasProjector;
        room.IsActive = dto.IsActive;
    }
    
    public static RoomDto ToDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        };
    }
}