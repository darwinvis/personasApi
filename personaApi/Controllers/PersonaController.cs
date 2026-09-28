using Microsoft.AspNetCore.Mvc;
using personaApi.DTOs;
using personaApi.Models;
using personaApi.Repositories;
using personaApi.Mappings;
namespace personaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonaController : ControllerBase
{
    private readonly IPersonaRepository _repository;

    public PersonaController(IPersonaRepository repository)
    {
        _repository = repository;
    }

    // GET: api/persona
    [HttpGet]
    public async Task<ActionResult<List<PersonaResponseDto>>> GetAll()
    {
        var personas = await _repository.GetAllAsync();

        var response = personas
            .Select(persona => persona.ToResponseDto())
            .ToList();

        return Ok(response);
    }

    // GET: api/persona/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaResponseDto>> GetById(string id)
    {
        var persona = await _repository.GetByIdAsync(id);

        if (persona is null)
            return NotFound();

        var response = persona.ToResponseDto();

        return Ok(response);
    }

    // POST: api/persona
    [HttpPost]
    public async Task<ActionResult<Persona>> Create(PersonaCreateDto dto)
    {
        var persona = dto.ToEntity();

        await _repository.CreateAsync(persona);

        return CreatedAtAction(
            nameof(GetById),
            new { id = persona.Id },
            persona);
    }

    // PUT: api/persona/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(
    string id,
    PersonaUpdateDto dto)
    {
        var existingPersona = await _repository.GetByIdAsync(id);

        if (existingPersona is null)
            return NotFound();

        var persona = dto.ToEntity(id);

        await _repository.UpdateAsync(id, persona);

        return NoContent();
    }

    // DELETE: api/persona/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var existingPersona = await _repository.GetByIdAsync(id);

        if (existingPersona is null)
            return NotFound();

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}