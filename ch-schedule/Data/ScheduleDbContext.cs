using ch_schedule.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch_schedule.Data;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options) { }

    public DbSet<CalendarItem> CalendarItems { get; set; } = null!;
    public DbSet<Attendee> Attendees { get; set; } = null!;
}