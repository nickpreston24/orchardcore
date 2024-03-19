using System.Text;
using System.Text.RegularExpressions;
using Bogus;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Dapper;
using Microsoft.Data.Sqlite;
using NSpecifications;

namespace Orchard.Sandbox.Services;

public class CalendarEventService : ICalendarEventService
{
    private readonly EmbeddedResourceService embeds;

    public CalendarEventService(IEmbeddedResourceQuery embed_service)
    {
        embeds = (EmbeddedResourceService?)embed_service;
        // SeedCalendar();
    }

    public async Task<int> SeedCalendar()
    {
        var connection = CreateConnection();
        string tablequery = embeds.GetFileContents<CalendarEventService>("SEED_CalendarEvents.sql").Dump();

        await connection.ExecuteAsync(tablequery);
        var fake_events = CreateFakeEvents().ToArray();

        var local_query = embeds.GetFileContents<CalendarEventService>("create_calendar_event.sql");

        var count = await Create(fake_events);
        Console.WriteLine($"{count} row(s) inserted.");
        return count;
    }

    private static string[] fake_event_names = new string[]
        { "Eat Dinner", "Build the Taj Mahal", "Reinvent the wheel", "Found SpaceX" };

    private static string[] fake_descriptions = new string[]
    {
        "Eat Dinner with the queen", "Build the Taj Mahal in one day",
        "Reinvent the wheel and build a time machine set to the stone age", "Found SpaceX before Elon Musk does..."
    };

    private List<CalendarEvent> CreateFakeEvents(int count = 10)
    {
        var index = 1;

        var calendar_faker = new Faker<CalendarEvent>()
                .CustomInstantiator(f => new CalendarEvent())
                .RuleFor(o => o.last_modified, f => f.Date.Recent(100))
                .RuleFor(o => o.created_at, f => f.Date.Recent(30))
                .RuleFor(o => o.event_name, f => fake_event_names.TakeFirstRandom())
                .RuleFor(o => o.event_name, f => fake_descriptions.TakeFirstRandom())
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
        var conn = new SqliteConnection("Data Source=LocalDatabase.db");
        return conn;
    }

    public async Task<CalendarEvent> GetById(int id)
    {
        using var connection = CreateConnection();
        var events_found =
            await connection.QueryAsync<CalendarEvent>("select * from CalendarEvents where id = @id");

        // Any validation logic can go here.  I'm just showing off what can easily be done.
        var has_valid_id = new Spec<CalendarEvent>(e => e.id > -1 && e.id == id);

        // There should only ever be ONE event with this specific Id.
        // This can easily be changed, if there's an edge case where we have duplicates in a merge or migration.
        // I'm just showing off NSpecification because it can do LINQ and compound specs (using & and |, see: https://github.com/ASbeletsky/NSpecifications)
        bool valid_event = events_found.Where(has_valid_id).SingleOrDefault() != default;

        if (valid_event)
        {
            // Perform the actual deletion
            var deleted_event_id =
                await connection.ExecuteAsync("delete from CalendarEvents where id = @id", new { id = id });
        }

        return events_found.SingleOrDefault();
    }

    public Task<List<CalendarEvent>> Search(Part search)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Create(params CalendarEvent[] records)
    {
        string sql = embeds.GetFileContents<CalendarEventService>("create_part.sql");
        records.Dump("creating parts");
        // https://regex101.com/r/XyhgkI/1
        // string full =
        //     @"(?<insert_clause>insert\s*into\s\w+\s*\([\w,\s*]+\))\s*(?<values_clause>values\s*\([@\w,\s]+\)\s*\;?)$";
        string values_clause = @"(?<values_clause>values\s*\([@\w,\s]+\)\s*\;?)$";
        // var values_regex = new Regex(values_clause,
        //     RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        string updated_sql = Regex.Replace(sql, values_clause, "",
            RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

        Console.WriteLine($"without values clause : >> {updated_sql}");

        string bulk_sql = records
            .ToList()
            .Aggregate(new StringBuilder("values ")
                    .Prepend(updated_sql)
                    .Prepend("begin transaction; \n")
                , (builder, calendarEvent) =>
                {
                    builder.Append("(");
                    builder.Append($"'{calendarEvent.id}', ");
                    builder.Append($"{calendarEvent.event_name}, ");
                    builder.Append($"'{calendarEvent.description}', ");
                    builder.Append($"'{calendarEvent.created_at}'");
                    builder.AppendLine("),");
                    return builder; //.RemoveFromEnd(2);
                })
            .RemoveFromEnd(2)
            .AppendLine("\n commit;")
            .ToString();

        Console.WriteLine($"bulk sql for {records.Length} records :>> " + bulk_sql);

        using var connection = CreateConnection();

        var record_count = connection.Execute(bulk_sql, records);
        Console.WriteLine("RECORDS CREATED : " + record_count);
        return record_count;
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