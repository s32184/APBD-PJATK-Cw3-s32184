namespace ExampleApp.Exceptions;

public class ReservationConflictException(int id) 
    : Exception($"Reservation for room id {id} have time conflicts with an existing reservation");