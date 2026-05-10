using ExampleApp.Exceptions;
using ExampleApp.Models;
using ExampleApp.Repositories;

namespace ExampleApp.Services;

public class ReservationService(
    IReservationRepository reservationRepository,
    IRoomRepository roomRepository) : IReservationService
{
    public IEnumerable<Reservation> GetAll(DateTime ? date, string? status, int? roomId)
    {
        var query = reservationRepository.GetAll().AsQueryable();
        
        if (date.HasValue)
            query = query.Where(r => r.Date.Date == date.Value.Date);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            query = query.Where(r => r.RoomId == roomId.Value);

        return query.ToList();
    }

    public Reservation GetById(int id)
    {
        var res = reservationRepository.GetById(id);
        if (res is null)
            throw new ReservationNotFoundException(id);

        return res;
    }

    public Reservation Add(Reservation reservation)
    {
        var room = roomRepository.GetById(reservation.RoomId);
        if (room is null)
            throw new RoomNotFoundException(reservation.RoomId);

        if (!room.IsActive)
            throw new RoomInactiveException(reservation.RoomId);

        if (HasConflict(reservation, null))
            throw new ReservationConflictException(reservation.RoomId);

        reservationRepository.Add(reservation);
        return reservation;
    }

    public Reservation Update(int id, Reservation reservation)
    {
        var existing = reservationRepository.GetById(id);
        if (existing is null)
            throw new ReservationNotFoundException(id);

        reservation.Id = id;

        var room = roomRepository.GetById(reservation.RoomId);
        if (room is null)
            throw new RoomNotFoundException(reservation.RoomId);

        if (!room.IsActive)
            throw new RoomInactiveException(reservation.RoomId);

        if (HasConflict(reservation, id))
            throw new ReservationConflictException(reservation.RoomId);

        if (!reservationRepository.Update(reservation))
            throw new ReservationNotFoundException(id);

        return reservation;
    }

    public void Remove(int id)
    {
        var existing = reservationRepository.GetById(id);
        if (existing is null)
            throw new ReservationNotFoundException(id);

        reservationRepository.Remove(existing);
    }

    private bool HasConflict(Reservation candidate, int? ignoreId)
    {
        var sameRoomSameDay = reservationRepository
            .GetByRoomAndDate(candidate.RoomId, candidate.Date);

        if (ignoreId.HasValue)
            sameRoomSameDay = sameRoomSameDay.Where(r => r.Id != ignoreId.Value);

        return sameRoomSameDay.Any(r =>
            candidate.StartTime < r.EndTime &&
            candidate.EndTime > r.StartTime);
    }
}