namespace Orchard.Sandbox.Services;

public interface ICalendarEventService
{
    Task<IEnumerable<CalendarEvent>> GetAll();
    Task<CalendarEvent> GetById(int id);
    Task<int> Create(string sql_file_path, params CalendarEvent[] records);
    Task<List<CalendarEvent>> Search(CalendarEvent search);
    Task Update(int id, CalendarEvent model);
    Task Delete(int id);
    Task<int> SeedCalendar();
    Task<int> CountExistingEvents();
    Task<int> DeleteAll();
}