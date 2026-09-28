using personaApi.DTOs;
using personaApi.Models;

namespace personaApi.Mappings;

public static class PersonaMapper
{
    public static PersonaResponseDto ToResponseDto(this Persona persona)
    {
        return new PersonaResponseDto
        {
            Id = persona.Id ?? string.Empty,
            Nombre = persona.Nombre,
            Apellido = persona.Apellido,

            Edad = persona.Edad,
            Ciudad = persona.Ciudad
        };
    }

    public static Persona ToEntity(this PersonaCreateDto dto)
    {
        return new Persona
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Edad = dto.Edad,
            Ciudad = dto.Ciudad
        };
    }

    public static Persona ToEntity(
        this PersonaUpdateDto dto,
        string id)
    {
        return new Persona
        {
            Id = id,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Edad = dto.Edad,
            Ciudad = dto.Ciudad
        };
    }
}