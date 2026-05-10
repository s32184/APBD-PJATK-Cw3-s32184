namespace ExampleApp.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string BuildingCode { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}