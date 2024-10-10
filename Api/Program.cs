using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entity;
using Security.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add db contexts
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(conn));

// map properties from appsettings.json
builder.Services.AddOptions<JwtOptions>().BindConfiguration(nameof(JwtOptions));
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

// configure JWT
builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddJwtBearer();

builder.Services.AddIdentityApiEndpoints<UserEntity>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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
app.MapGroup("/user").MapIdentityApi<UserEntity>();

// run Persistence migrations during start up
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Migrate();
}

app.Run();