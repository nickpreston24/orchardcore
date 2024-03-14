namespace Orchard.Sandbox.Services;

public interface ICalendarEventService
{
    Task<IEnumerable<CalendarEvent>> GetAll();
    Task<CalendarEvent> GetById(int id);
    Task Create(CalendarEvent model);
    Task Update(int id, CalendarEvent model);
    Task Delete(int id);
}