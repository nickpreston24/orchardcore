namespace Orchard.Sandbox.Services;

public interface ICalendarEventService
{
    Task<IEnumerable<CalendarEvent>> GetAll();
    Task<CalendarEvent> GetById(int id);
    Task<int> Create(params CalendarEvent[] records);
    Task<List<CalendarEvent>> Search(Part search);
    Task Update(int id, CalendarEvent model);
    Task Delete(int id);
}