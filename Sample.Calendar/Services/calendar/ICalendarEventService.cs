using System.Collections.Generic;
using System.Threading.Tasks;
using OrchardCore.Models;

namespace OrchardCore.Services;

public interface ICalendarEventService
{
    Task<IEnumerable<CalendarEvent>> GetAll();
    Task<CalendarEvent> GetById(int id);
    Task<int> Create(string sql_file_path, params CalendarEvent[] records);
    Task<List<CalendarEvent>> Search(CalendarEvent search);
    Task Update(int id, CalendarEvent model);
    Task<int> Delete(int id);
    Task<int> SeedCalendar(int limit = 20);
    Task<int> CountExistingEvents();
    Task<int> DeleteAll();
}