using ExampleApp.Models;

namespace ExampleApp.Repositories;

public class ReservationRepository : IReservationRepository
{
    private static int _nextId = 1;
    private readonly List<Reservation> _reservations = [];

    public ReservationRepository()
    {
        _reservations.AddRange(new[]
        {
            new Reservation
            {
                Id = _nextId++,
                RoomId = 1,
                OrganizerName = "Jan Nowak",
                Topic = "Wprowadzenie do C#",
                Date = new DateTime(2026, 5, 10),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = _nextId++,
                RoomId = 2,
                OrganizerName = "Anna Kowalska",
                Topic = "Warsztaty z HTTP i REST",
                Date = new DateTime(2026, 5, 10),
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 30, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = _nextId++,
                RoomId = 4,
                OrganizerName = "Jan Pawel",
                Topic = "Wprowadzenie do C++",
                Date = new DateTime(2026, 7, 20),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                Status = "cancelled"
            },
            new Reservation
            {
                Id = _nextId++,
                RoomId = 3,
                OrganizerName = "Anna Ptak",
                Topic = "Warsztaty z Javy",
                Date = new DateTime(2026, 6, 17),
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 30, 0),
                Status = "cancelled"
            }
        });
    }

    public IEnumerable<Reservation> GetAll() => _reservations;

    public Reservation? GetById(int id) => _reservations.FirstOrDefault(r => r.Id == id);

    public void Add(Reservation reservation)
    {
        reservation.Id = _nextId++;
        _reservations.Add(reservation);
    }

    public bool Update(Reservation reservation)
    {
        var existing = GetById(reservation.Id);
        if (existing is null) return false;

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return true;
    }

    public void Remove(Reservation reservation) => _reservations.Remove(reservation);

    public IEnumerable<Reservation> GetByRoomAndDate(int roomId, DateTime  date) =>
        _reservations.Where(r => r.RoomId == roomId && r.Date.Date == date.Date);

}