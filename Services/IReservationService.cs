using ExampleApp.Models;

namespace ExampleApp.Services;

public interface IReservationService
{
    IEnumerable<Reservation> GetAll(DateTime? date, string? status, int? roomId);
    Reservation GetById(int id);
    Reservation Add(Reservation reservation);
    Reservation Update(int id, Reservation reservation);
    void Remove(int id);
}