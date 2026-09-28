using System.ComponentModel.DataAnnotations;

namespace personaApi.DTOs;

public class PersonaUpdateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Apellido { get; set; } = string.Empty;

    [Range(1, 120)]
    public int Edad { get; set; }

    [Required]
    public string Ciudad { get; set; } = string.Empty;
}