namespace Orchard.Sandbox.Services;

public class CalendarEvent
{
    public string event_name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public int id { get; set; }
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public TimeSpan duration { get; set; }
    public DateTime created_at { get; set; }
    public DateTime last_modified { get; set; }
}