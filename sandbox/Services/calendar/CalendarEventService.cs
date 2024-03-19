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
                , (builder, part) =>
                {
                    builder.Append("(");
                    // builder.Append($"'{part.Name}', ");
                    // builder.Append($"{part.Cost}, ");
                    // builder.Append($"'{part.Kind}', ");
                    // builder.Append($"'{part.Manufacturer}'");
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