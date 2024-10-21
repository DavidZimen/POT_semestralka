using System.Security.Claims;
using Domain.Extensions;
using Persistence.Extensions;
using Security.Extension;

var builder = WebApplication.CreateBuilder(args);

// configure options for Keycloak provider and add it
builder.ConfigureKeycloakForApi()
    .ConfigureKeycloakServer()
    .AddKeycloakToApi();

// configure database with migrations
builder.ConfigureDatabase();

// add mappers between Dto and entities
builder.AddMappers();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerGenWithAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("users/me", (ClaimsPrincipal cp) =>
{
    return cp.Claims.DistinctBy(c => c.Type).ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();