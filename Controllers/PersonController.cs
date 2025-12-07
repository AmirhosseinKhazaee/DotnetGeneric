using Microsoft.AspNetCore.Mvc;
using XGeneric.Base;
using XGeneric.Extensions;
using XGeneric.Models;
using XGeneric.Repository;

namespace XGeneric.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IGenericRepo<Person> _personRepo;
    private readonly IGenericRepo<Tasks> _taskRepo;

    public PersonController(
        IGenericRepo<Person> personRepo,
        IGenericRepo<Tasks> taskRepo)
    {
        _personRepo = personRepo;
        _taskRepo = taskRepo;
    }
    [HttpGet("getall")]
    public async Task<IEnumerable<Person>> GetAll()
    {
        var result = await _personRepo.GetAllAsync();
        return result;
    }


    // Async Get Task
    [HttpGet("task/{id:guid}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var task = await _taskRepo.GetByIdAsync(id);
        if (task == null) return NotFound("Task not found");
        return Ok(task);
    }

    // Async Create Person
    [HttpPost]
    public async Task<IActionResult> CreatePerson(Person model)
    {
        if (model == null)
            return BadRequest("Invalid data");

        model.IsXBaseModel();

        var success = await _personRepo.AddAsync(model);
        if (!success)
            return Conflict("A person with this ID already exists");

        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    // Async Get Person
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await _personRepo.GetByIdAsync(id);
        if (person == null) return NotFound("Person not found");
        return Ok(person);
    }

    // Async Update Person
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePerson(Guid id, Person model)
    {
        if (model == null) return BadRequest("Invalid data");

        var existing = await _personRepo.GetByIdAsync(id);
        if (existing == null) return NotFound("Person not found");

        // Update fields
        existing.Username = model.Username;
        existing.Firstname = model.Firstname;
        existing.Lastname = model.Lastname;
        existing.UpdatedAt = DateTime.UtcNow;

        var success = await _personRepo.UpdateAsync(existing);
        if (!success) return StatusCode(500, "Cannot update person");

        return Ok(existing);
    }

    // Async Delete Person
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var success = await _personRepo.DeleteAsync(id);
        if (!success) return NotFound("Person not found");
        return Ok("Person deleted");
    }
}
