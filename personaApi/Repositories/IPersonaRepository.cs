using personaApi.Models;

namespace personaApi.Repositories;

public interface IPersonaRepository
{
    Task<List<Persona>> GetAllAsync();

    Task<Persona?> GetByIdAsync(string id);

    Task CreateAsync(Persona persona);

    Task UpdateAsync(string id, Persona persona);

    Task DeleteAsync(string id);
}