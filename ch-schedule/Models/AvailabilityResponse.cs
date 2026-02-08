using System.Text.Json.Serialization;

namespace ch_schedule.Models;

public class AvailabilityResponse
{
    public bool Found { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? Start { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? End { get; set; }
}