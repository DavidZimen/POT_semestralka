using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Extension;
using Security.Config;

var builder = WebApplication.CreateBuilder(args);

// configure options for Keycloak provider and add it
builder.Services
    .ConfigureKeycloakForApi()
    .ConfigureKeycloakServer()
    .AddKeycloakToApi();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add db contexts
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
        options.UseNpgsql(conn, x => 
            x.MigrationsHistoryTable("__EFMigrationsHistory", ApplicationDbContext.ApplicationSchema)
            )
        )
    .AddMigrationService(o => o.RunMigrationsOnStartup = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("users/me", (ClaimsPrincipal cp) =>
{
    return cp.Claims.ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();