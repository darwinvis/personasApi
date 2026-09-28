using Microsoft.Extensions.Options;
using MongoDB.Driver;
using personaApi.Configuration;
using personaApi.Repositories;
using personaApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CORS - Permitir Angular
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ==========================================
// Configuración de MongoDB
// ==========================================
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp
        .GetRequiredService<IOptions<MongoDbSettings>>()
        .Value;

    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp
        .GetRequiredService<IOptions<MongoDbSettings>>()
        .Value;

    var client = sp.GetRequiredService<IMongoClient>();

    return client.GetDatabase(settings.DatabaseName);
});

// ==========================================
// Repository
// ==========================================
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

// ==========================================
// Controllers
// ==========================================
builder.Services.AddControllers();

// ==========================================
// Swagger
// ==========================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==========================================
// HTTPS
// ==========================================
app.UseHttpsRedirection();

// ==========================================
// CORS
// IMPORTANTE: antes de Controllers/Endpoints
// ==========================================
app.UseCors("AngularPolicy");

// ==========================================
// Middleware de excepciones
// ==========================================
app.UseMiddleware<ExceptionMiddleware>();

// ==========================================
// Authorization
// ==========================================
app.UseAuthorization();

// ==========================================
// Controllers
// ==========================================
app.MapControllers();

// ==========================================
// Swagger
// ==========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();