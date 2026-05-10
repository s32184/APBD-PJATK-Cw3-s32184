using ExampleApp.Exceptions;
using ExampleApp.Models;
using ExampleApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IReservationService reservationService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateTime? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var reservations = reservationService.GetAll(date, status, roomId);
        return Ok(reservations);
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById(int id)
    {
        try
        {
            var res = reservationService.GetById(id);
            return Ok(res);
        }
        catch (ReservationNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        try
        {
            var created = reservationService.Add(reservation);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (RoomNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (RoomInactiveException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ReservationConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    
    [HttpPut("{id:int}")]
    public ActionResult<Reservation> Update(int id, [FromBody] Reservation reservation)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        try
        {
            var updated = reservationService.Update(id, reservation);
            return Ok(updated);
        }
        catch (ReservationNotFoundException)
        {
            return NotFound();
        }
        catch (RoomNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (RoomInactiveException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ReservationConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            reservationService.Remove(id);
            return NoContent();
        }
        catch (ReservationNotFoundException)
        {
            return NotFound();
        }
    }
}