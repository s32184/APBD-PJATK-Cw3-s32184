namespace ExampleApp.Exceptions;

public class RoomNotFoundException(int id) 
    : Exception($"Room with id {id} not found");