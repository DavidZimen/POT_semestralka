using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Extension;
using Security.Extension;

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
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(conn))
    .AddMigrationService(true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("DEVELOPMENT");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();