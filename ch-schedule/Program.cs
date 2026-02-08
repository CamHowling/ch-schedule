using ch_schedule.Data;
using ch_schedule.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICalendarService, CalendarService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register EF Core InMemory
builder.Services.AddDbContext<ScheduleDbContext>(options =>
    options.UseInMemoryDatabase("ScheduleDb"));

var app = builder.Build();

SeedData.InitializeInMemDb(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


