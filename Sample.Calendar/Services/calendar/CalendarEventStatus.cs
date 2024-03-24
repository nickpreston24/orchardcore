using System;
using System.Linq;
using CodeMechanic.Types;

namespace OrchardCore.Models;

/// <summary>
/// Read the Enumeration Documentation.  This is something Microsoft suggested, and something I agree could help make enums easier in the DB, etc.
/// Usage:
///     Just extend this class to hold whatever values you need.
///     Personally, I like to use Enumeration to define static compiled Regex patterns so I can reference them by name.
/// Methods:
///     GetAll<T>() will list all the Enumerations you have defined.
/// </summary>
public class CalendarEventStatus : Enumeration
{
    // as the name implies, we don't know the status of the event.
    public static CalendarEventStatus Unknown = new CalendarEventStatus(1, nameof(Unknown));

    // Requirement: "I can visit a page /calendar and see a calendar with all published events"
    public static CalendarEventStatus Published = new CalendarEventStatus(2, nameof(Published));

    // This is how we might convert a string from our DB into the current status, easily.
    // No more storing ints in the db (unless we wanna)
    public static implicit operator CalendarEventStatus(string name)
    {
        return GetAll<CalendarEventStatus>()
            .SingleOrDefault(ev => ev.ToString().Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public CalendarEventStatus(int id, string name) : base(id, name)
    {
    }
}