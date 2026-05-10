namespace ExampleApp.Exceptions;

public class RoomInactiveException(int id) 
    : Exception($"Room with id {id} is inactive and cannot be reserved");