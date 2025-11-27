using Microsoft.AspNetCore.Mvc;
using XGeneric.Base;
using XGeneric.Models;
using XGeneric.Repository;

namespace XGeneric.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IGenericRepo<Person> _personRepo;

        public PersonController(IGenericRepo<Person> personRepo)
        {
            _personRepo = personRepo;
        }

        // GET: api/person
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _personRepo.GetAll();
            return Ok(result);
        }

        // GET: api/person/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var result = _personRepo.GetById(id);

            if (result == null)
                return NotFound("Person not found");

            return Ok(result);
        }

        // POST: api/person
        [HttpPost]
        public IActionResult Create(Person model)
        {
            if (model == null)
                return BadRequest("Invalid data");

            var success = _personRepo.Add(model);

            if (!success)
                return Conflict("A person with this ID already exists");

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/person/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, Person model)
        {
            if (model == null)
                return BadRequest("Invalid data");

            var existing = _personRepo.GetById(id);

            if (existing == null)
                return NotFound("Person not found");

            // update fields
            existing.Username = model.Username;
            existing.Firstname = model.Firstname;
            existing.Lastname = model.Lastname;

            var success = _personRepo.Update(existing);

            if (!success)
                return StatusCode(500, "Cannot update person");

            return Ok(existing);
        }

        // DELETE: api/person/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var success = _personRepo.Delete(id);

            if (!success)
                return NotFound("Person not found");

            return Ok("Person deleted");
        }
    }
}
