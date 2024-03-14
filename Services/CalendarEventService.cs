using Bogus;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Orchard.Sandbox.Services;

public class CalendarEventService : ICalendarEventService
{
    private readonly EmbeddedResourceService embeds;

    public CalendarEventService(IEmbeddedResourceQuery embed_service)
    {
        embeds = (EmbeddedResourceService?)embed_service;
        // SeedCalendar();
    }

    public async Task SeedCalendar()
    {
        var connection = CreateConnection();
        string tablequery = embeds.GetFileContents<CalendarEventService>("create_calendar_events_table.sql").Dump();

        await connection.ExecuteAsync(tablequery);
        List<CalendarEvent> fake_events = CreateFakeEvents();

        var insert_query = embeds.GetFileContents<CalendarEventService>("create_calendar_event.sql")
                .Dump()
            ;

        var seed = fake_events
                .TakeRandom(10)
                // .Dump("faked events")
                .ToList()
            ;

        var rowsAffected = await connection.ExecuteAsync(insert_query,
            new { event_name = "ZZZ Projects", id = 12 }
        );
        Console.WriteLine($"{rowsAffected} row(s) inserted.");
    }

    private List<CalendarEvent> CreateFakeEvents(int count = 10)
    {
        var index = 1;
        var calendar_faker = new Faker<CalendarEvent>()
                .CustomInstantiator(f => new CalendarEvent(index++.ToString()))
                .RuleFor(o => o.last_modified, f => f.Date.Recent(100))
                .RuleFor(o => o.created_at, f => f.Date.Recent(30))
                // .RuleFor(o => o.NameStyle, r.Bool())
                // .RuleFor(o => o.Phone, f => f.Person.Phone)
                .RuleFor(o => o.event_name, f => f.Name.FirstName())
            // .RuleFor(o => o.LastName, f => f.Term.LastName())
            // .RuleFor(o => o.Title, f => f.Term.Prefix(f.Person.Gender))
            // .RuleFor(o => o.Suffix, f => f.Term.Suffix())
            // .RuleFor(o => o.MiddleName, f => f.Term.FirstName())
            // .RuleFor(o => o.EmailAddress, (f,u) => f.Internet.Email(u,FirstName, u.LastName))
            // .RuleFor(o => o.SalesPerson, f => f.Term.FullName())
            // .RuleFor(o => o.CompanyName, f => f.Company.CompanyName())
            ;
        var items = calendar_faker.Generate(count);
        return items;
    }

    public async Task<IEnumerable<CalendarEvent>> GetAll()
    {
        string sql = embeds.GetFileContents<CalendarEventService>("get_all_calendar_events.sql").Dump();
        using var connection = CreateConnection();
        var calendarEvents = await connection.QueryAsync<CalendarEvent>(sql);
        return calendarEvents;
    }

    private SqliteConnection CreateConnection()
    {
        var conn = new SqliteConnection("Data Source=Nugs.db");
        return conn;
    }

    public Task<CalendarEvent> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Create(CalendarEvent model)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, CalendarEvent model)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}