using ExampleApp.Models;

namespace ExampleApp.Repositories;

public interface IReservationRepository
{
    IEnumerable<Reservation> GetAll();
    Reservation? GetById(int id);
    void Add(Reservation reservation);
    bool Update(Reservation reservation);
    void Remove(Reservation reservation);
    IEnumerable<Reservation> GetByRoomAndDate(int roomId, DateTime  date);
}