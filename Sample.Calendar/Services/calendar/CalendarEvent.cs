using System;

namespace OrchardCore.Models;

public class CalendarEvent
{
    public string event_name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public int id { get; set; }
    public DateTime start_date { get; set; } = DateTime.UtcNow;
    public DateTime end_date { get; set; }
    public TimeSpan duration { get; set; }
    public DateTime created_at { get; set; }
    public DateTime last_modified { get; set; }

    public string event_status_badge => new CalendarEventStatus(-1, this.status).Equals(CalendarEventStatus.Published)
        ? "badge badge-success"
        : "badge badge-warning";

    public string EditMode { get; set; } = "Add"; // lazy, i know.
    public string ViewName { get; set; } = "Calendar";
}