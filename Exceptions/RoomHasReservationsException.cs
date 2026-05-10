namespace ExampleApp.Exceptions;

public class RoomHasReservationsException(int id) 
    : Exception($"Room with id {id} cannot be deleted because it has reservations");