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

    // Inject both repositories
    public PersonController(
        IGenericRepo<Person> personRepo,
        IGenericRepo<Tasks> taskRepo)
    {
        _personRepo = personRepo;
        _taskRepo = taskRepo;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        var person = new Person();

        if (person.IsXBaseModel())
        {
            return Ok(new
            {
                Model = person.GetType().Name,
                CreatedAt = person.CreatedAt,
                UpdatedAt = person.UpdatedAt
            });
        }

        return BadRequest("Model is not XBaseModel");
    }

    [HttpPost("test2")]
    public object CreateTask(Tasks model)
    {
        if (model == null)
            return BadRequest("Invalid data");

        // Initialize XBaseModel metadata
        model.IsXBaseModel();

        var success = _taskRepo.Add(model);

        if (!success)
            return Conflict("A task with this ID already exists");

        return "ok";
    }

    [HttpGet("task/{id:guid}")]
    public IActionResult GetTaskById(Guid id)
    {
        var task = _taskRepo.GetById(id);
        if (task == null) return NotFound("Task not found");
        return Ok(task);
    }

    [HttpPost]
    public IActionResult CreatePerson(Person model)
    {
        if (model == null)
            return BadRequest("Invalid data");

        model.IsXBaseModel();

        var success = _personRepo.Add(model);

        if (!success)
            return Conflict("A person with this ID already exists");

        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var person = _personRepo.GetById(id);
        if (person == null) return NotFound("Person not found");
        return Ok(person);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdatePerson(Guid id, Person model)
    {
        if (model == null) return BadRequest("Invalid data");

        var existing = _personRepo.GetById(id);
        if (existing == null) return NotFound("Person not found");

        // Update fields
        existing.Username = model.Username;
        existing.Firstname = model.Firstname;
        existing.Lastname = model.Lastname;
        existing.UpdatedAt = DateTime.UtcNow;

        var success = _personRepo.Update(existing);
        if (!success) return StatusCode(500, "Cannot update person");

        return Ok(existing);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePerson(Guid id)
    {
        var success = _personRepo.Delete(id);
        if (!success) return NotFound("Person not found");
        return Ok("Person deleted");
    }
}
