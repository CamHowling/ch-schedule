namespace ch_schedule.Entities;
public class CalendarItem
{
    public Guid Id { get; set; }
    public Guid AttendeeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}
