using MongoDB.Driver;
using personaApi.Models;

namespace personaApi.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly IMongoCollection<Persona> _personas;

    public PersonaRepository(IMongoDatabase database)
    {
        _personas = database.GetCollection<Persona>("persona");
    }

    public async Task<List<Persona>> GetAllAsync()
    {
        return await _personas
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<Persona?> GetByIdAsync(string id)
    {
        return await _personas
            .Find(persona => persona.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Persona persona)
    {
        await _personas.InsertOneAsync(persona);
    }

    public async Task UpdateAsync(string id, Persona persona)
    {
        await _personas.ReplaceOneAsync(
            p => p.Id == id,
            persona);
    }

    public async Task DeleteAsync(string id)
    {
        await _personas.DeleteOneAsync(
            persona => persona.Id == id);
    }
}