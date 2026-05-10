using System.ComponentModel.DataAnnotations;

namespace ExampleApp.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "RoomId must be a positive number.")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "OrganizerName is required.")]
    public required string OrganizerName { get; set; }

    [Required(ErrorMessage = "Topic is required.")]
    public required string Topic { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "StartTime is required.")]
    public TimeSpan StartTime { get; set; }
    
    [Required(ErrorMessage = "EndTime is required.")]
    public TimeSpan EndTime { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; } = "planned";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be later than StartTime.",
                new[] { nameof(EndTime) });
        }
        var allowedStatuses = new[] { "planned", "confirmed", "cancelled" };

        if (!allowedStatuses.Contains(Status, StringComparer.OrdinalIgnoreCase))
        {
            yield return new ValidationResult(
                "Status must be one of: planned, confirmed, cancelled.",
                new[] { nameof(Status) });
        }
    }
}