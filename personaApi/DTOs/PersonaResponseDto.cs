namespace personaApi.DTOs;

public class PersonaResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Ciudad { get; set; } = string.Empty;
}