using ExampleApp.DTOs;
using ExampleApp.Exceptions;
using ExampleApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly
    )
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var room = service.GetAll(minCapacity, hasProjector, activeOnly);
        return Ok(room);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(service.GetById(id));
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding(string buildingCode)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var rooms = service.GetByBuilding(buildingCode).ToList();
        return Ok(rooms);
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] CreateRoomDto room)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var createdRoom = service.Add(room);
        return CreatedAtAction(
            nameof(GetById), 
            new { id = createdRoom.Id },
            createdRoom
        );
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateRoomDto room)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        try
        {
            return Ok(service.Update(id, room));
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            service.Remove(id);
            return NoContent();
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (RoomHasReservationsException e)
        {
            return Conflict(e.Message);
        }
    }
}