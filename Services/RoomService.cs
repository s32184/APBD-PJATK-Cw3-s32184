using ExampleApp.DTOs;
using ExampleApp.Exceptions;
using ExampleApp.Mappers;
using ExampleApp.Repositories;

namespace ExampleApp.Services;

public class RoomService(
    IRoomRepository repository,
    IReservationRepository reservationRepository) : IRoomService
{
    public IEnumerable<RoomDto> GetAll(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        var rooms = repository.GetAll();

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            rooms = rooms.Where(r => r.IsActive);

        return rooms.Select(r => r.ToDto());
    }

    public RoomDto GetById(int id)
    {
        var room = repository.GetById(id);

        if (room is null)
            throw new RoomNotFoundException(id);

        return room.ToDto();
    }
    
    public IEnumerable<RoomDto> GetByBuilding(string buildingCode)
    {
            return repository
                .GetByBuilding(buildingCode)
                .Select(r => r.ToDto());
    }
    
    public RoomDto Add(CreateRoomDto room)
    {
        var roomToAdd = room.ToDomain();
        repository.Add(roomToAdd);
        
        return roomToAdd.ToDto();
    }

    public RoomDto Update(int id, UpdateRoomDto dto)
    {
        var existing = repository.GetById(id);
        if (existing is null)
            throw new RoomNotFoundException(id);

        dto.MapTo(existing);

        repository.Update(existing);

        return existing.ToDto();
    }



    public void Remove(int id)
    {
            var room = repository.GetById(id);
            if (room is null)
                throw new RoomNotFoundException(id);
            
            if (reservationRepository.GetAll()
                .Any(r => r.RoomId == id && r.Date >= DateTime.Today))
            {
                throw new RoomHasReservationsException(id);
            }
            
            repository.Remove(room);
    }
}