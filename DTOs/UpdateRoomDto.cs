using System.ComponentModel.DataAnnotations;

namespace ExampleApp.DTOs;

public class UpdateRoomDto
{
    [MaxLength(100), Required(ErrorMessage = "Name is required.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "BuildingCode is required.")]
    public required string BuildingCode { get; set; }

    public int Floor { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}