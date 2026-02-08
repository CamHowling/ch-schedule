using ch_schedule.Data;
using ch_schedule.Entities;
using ch_schedule.Models;
using ch_schedule.Services;
using Microsoft.AspNetCore.Mvc;

namespace ch_schedule.Controllers
{
    [ApiController]
    [Route("/")]
    public class CalendarController(ICalendarService calendarService) : ControllerBase
    {
        [HttpPost]
        [Route("availability")]
        public async Task<AvailabilityResponse> GetAvailability([FromBody] AvailabilityInput input)
        {

            var response = await calendarService.GetFirstAvailableInterval(input);
            return response;
        }

        [HttpPut]
        [Route("calendars/{personId}/busy")]
        public async Task<AddCalendarItemResponse> AddCalendarItem(Guid personId, [FromBody] AddCalendarItemInput item)
        {
            var calendarItem = new CalendarItem
            {
                Id = Guid.NewGuid(),
                AttendeeId = personId,
                Start = item.Start,
                End = item.End
            };

            await calendarService.AddCalendarItem(calendarItem);
            return new AddCalendarItemResponse
            {
                Id = calendarItem.Id,
                AttendeeId = calendarItem.AttendeeId,
                Start = calendarItem.Start,
                End = calendarItem.End
            };

        }
    }
}
