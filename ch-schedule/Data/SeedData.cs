using ch_schedule.Entities;

namespace ch_schedule.Data;

public static class SeedData
{
    public static void InitializeInMemDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ScheduleDbContext>();

        var alice = new Attendee
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Alice"
        };

        var bob = new Attendee
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Bob"
        };

        var charlie = new Attendee
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Charlie"
        };

        var diana = new Attendee
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Name = "Diana"
        };

        var ethan = new Attendee
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Name = "Ethan"
        };

        db.Attendees.Add(alice);
        db.Attendees.Add(bob);
        db.Attendees.Add(charlie);
        db.Attendees.Add(diana);
        db.Attendees.Add(ethan);

        var attendeeMap = new Dictionary<int, Guid>
        {
            [1] = alice.Id,
            [2] = bob.Id,
            [3] = charlie.Id,
            [4] = diana.Id,
            [5] = ethan.Id,
        };

        var seedItems = new (int AttendeeNum, string StartIso, string EndIso)[]
        {
            (1, "2026-01-01T10:30:00+00:00", "2026-01-01T11:30:00+00:00"),
            (1, "2026-01-01T10:00:00+00:00", "2026-01-01T10:20:00+00:00"),
            (1, "2026-01-01T11:20:00+00:00", "2026-01-01T12:20:00+00:00"),
            (2, "2026-01-01T11:30:00+00:00", "2026-01-01T12:30:00+00:00"),
            (2, "2026-01-01T11:00:00+00:00", "2026-01-01T11:20:00+00:00"),
            (2, "2026-01-01T12:20:00+00:00", "2026-01-01T13:20:00+00:00"),
            (3, "2026-01-01T12:30:00+00:00", "2026-01-01T13:30:00+00:00"),
            (3, "2026-01-01T12:00:00+00:00", "2026-01-01T12:20:00+00:00"),
            (3, "2026-01-01T13:20:00+00:00", "2026-01-01T14:20:00+00:00"),
            (4, "2026-01-01T13:30:00+00:00", "2026-01-01T14:30:00+00:00"),
            (4, "2026-01-01T13:00:00+00:00", "2026-01-01T13:20:00+00:00"),
            (4, "2026-01-01T14:20:00+00:00", "2026-01-01T15:20:00+00:00"),
            (5, "2026-01-01T14:30:00+00:00", "2026-01-01T15:30:00+00:00"),
            (5, "2026-01-01T14:00:00+00:00", "2026-01-01T14:20:00+00:00"),
            (5, "2026-01-01T15:20:00+00:00", "2026-01-01T16:20:00+00:00"),
        };

        foreach (var (num, startIso, endIso) in seedItems)
        {
            if (!attendeeMap.TryGetValue(num, out var attendeeId))
                continue;

            db.CalendarItems.Add(new CalendarItem
            {
                Id = Guid.NewGuid(),
                AttendeeId = attendeeId,
                Title = "Existing meeting",
                Start = DateTimeOffset.Parse(startIso),
                End = DateTimeOffset.Parse(endIso)
            });
        }

        db.SaveChanges();
    }
}
