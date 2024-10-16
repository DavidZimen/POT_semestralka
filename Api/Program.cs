using Microsoft.EntityFrameworkCore;
using Persistence;
using Security;

var builder = WebApplication.CreateBuilder(args);

// configure options for Keycloak provider and add it
builder.Services
    .ConfigureKeycloakForApi()
    .AddKeycloakToApi();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add db contexts
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(conn));

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

using (var scope = app.Services.CreateScope())
{
    // run Persistence migrations during start up
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Migrate();
    
    // create keycloak realm if it does not exists
}

app.Run();