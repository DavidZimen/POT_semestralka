using Api.Extensions;
using Domain.Extensions;
using Persistence.Extensions;
using Security.Server.Extension;

var builder = WebApplication.CreateBuilder(args);

// configure options for Keycloak provider and add it
builder.ConfigureKeycloakForApi()
    .ConfigureKeycloakServer()
    .AddKeycloakToApi();

// configure database with migrations and add repositories
builder.ConfigureDatabase()
    .AddRepositories();

// add mappers between Dto and entities
builder.AddMappers();

// add CORS to the app
builder.AddCors();

// add services
builder.AddServices();

// Add endpoints to the container.
builder.Services.AddControllers();

// add exception handlers
builder.AddExceptionHandlers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGenWithAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();