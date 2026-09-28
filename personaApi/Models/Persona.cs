using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace personaApi.Models;

public class Persona
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [BsonElement("apellido")]
    public string Apellido { get; set; } = string.Empty;

    [BsonElement("edad")]
    public int Edad { get; set; }

    [BsonElement("ciudad")]
    public string Ciudad { get; set; } = string.Empty;
}
