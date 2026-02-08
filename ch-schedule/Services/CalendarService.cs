using ch_schedule.Data;
using ch_schedule.Entities;
using ch_schedule.Models;
using Microsoft.EntityFrameworkCore;

namespace ch_schedule.Services;

public class CalendarService(ScheduleDbContext db) : ICalendarService
{    
    public async Task<AvailabilityResponse> GetFirstAvailableInterval(AvailabilityInput input)
    {
        var clampedAttendeeItems = await db.CalendarItems
            .Where(x => input.AttendeeIds.Contains(x.AttendeeId) &&
                    x.Start < input.End && x.End > input.Start)
            .OrderBy(x => x.Start)
            .ToListAsync();

        var mergedBusyTimes = new List<(DateTimeOffset Start, DateTimeOffset End)>();
        foreach (var item in clampedAttendeeItems)
        {
            if (mergedBusyTimes.Count == 0)
            {
                mergedBusyTimes.Add((item.Start, item.End));
                continue;
            }

            var last = mergedBusyTimes[^1];
            if (item.Start <= last.End)
            {
                mergedBusyTimes[^1] = (last.Start, item.End > last.End ? item.End : last.End);
            }
            else
            {
                mergedBusyTimes.Add((item.Start, item.End));
            }
        }

        if (mergedBusyTimes.Count == 0)
            return Found(input.Start, input.End);

        if (input.Start.Add(input.Duration) < mergedBusyTimes[0].Start)
            return Found(input.Start, input.Start.Add(input.Duration));

        for (int i = 0; i < mergedBusyTimes.Count - 1; i++)
        {
            var gapStart = mergedBusyTimes[i].End;
            var gapEnd = mergedBusyTimes[i + 1].Start;

            if (gapStart.Add(input.Duration) < gapEnd) 
                return Found(gapStart, gapStart.Add(input.Duration));
        }

        var lastEnd = mergedBusyTimes[^1].End;

        if (lastEnd.Add(input.Duration) < input.End)
            return Found(lastEnd, lastEnd.Add(input.Duration));

        return NotFound();

    }
    
    public async Task AddCalendarItem(CalendarItem item)
    {
        db.CalendarItems.Add(item);
        await db.SaveChangesAsync();
    }

    private static AvailabilityResponse Found(DateTimeOffset start, DateTimeOffset end)
        => new() { Found = true, Start = start, End = end };

    private static AvailabilityResponse NotFound()
        => new() { Found = false, Start = null, End = null };
}
