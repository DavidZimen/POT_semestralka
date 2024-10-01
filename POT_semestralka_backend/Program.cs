using Microsoft.EntityFrameworkCore;
using POT_semestralka_backend.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add db contexts
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbMigrationContext>(options => options.UseNpgsql(conn));
builder.Services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(conn));
builder.Services.AddDbContext<HotelDbContext>(options => options.UseNpgsql(conn));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// run database migrations during start up
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DbMigrationContext>();
    db.Migrate();
}

app.Run();