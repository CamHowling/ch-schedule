namespace ch_schedule.Models
{
    public class AddCalendarItemInput
    {
        public Guid AttendeeId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
    }
}
