using CodeMechanic.Types;

namespace Orchard.Sandbox.Services;

public class CalendarEvent
{
    public CalendarEvent(string id, string eventName = "")
    {
        this.id = id.ToInt();
        event_name = eventName;
    }

    public string event_name { get; set; } = string.Empty;

    public int id { get; set; }
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public TimeSpan duration { get; set; }
    public DateTime created_at { get; set; }
    public DateTime last_modified { get; set; }
}