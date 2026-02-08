using ch_schedule.Entities;
using ch_schedule.Models;

namespace ch_schedule.Services;

public interface ICalendarService
{
    Task<AvailabilityResponse> GetFirstAvailableInterval(AvailabilityInput input);
    Task AddCalendarItem(CalendarItem item);
}
