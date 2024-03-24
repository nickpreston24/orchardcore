using System;
using System.Linq;
using CodeMechanic.Types;

namespace OrchardCore.Models;

// I realize there's different roles, but idk how to use them in OrchardCore.  so, I'm just providing castable Enums to stand in for it.
public class CalendarViewMode : Enumeration
{
    public static CalendarViewMode Admin = new CalendarViewMode(1, nameof(Admin));
    public static CalendarViewMode User = new CalendarViewMode(2, nameof(User));

    public static implicit operator CalendarViewMode(string name) => GetAll<CalendarViewMode>()
        .SingleOrDefault(ev => ev.ToString()
            .Equals(name, StringComparison.OrdinalIgnoreCase));

    public CalendarViewMode(int id, string name) : base(id, name)
    {
    }
}