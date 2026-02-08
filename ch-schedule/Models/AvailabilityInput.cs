namespace ch_schedule.Models
{
    public class AvailabilityInput
    {
        public List<Guid> AttendeeIds { get; set; } = [];
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
