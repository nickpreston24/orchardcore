using System.Data;
using System.Data.SQLite;
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
    }

    public async Task<int> CountExistingEvents()
    {
        using var connection = CreateConnection();

        string sql = "select count (id) from CalendarEvents;";
        int count = await connection.ExecuteScalarAsync<int>(sql);
        return count;
    }

    public async Task<int> SeedCalendar()
    {
        int LIMIT = 20;
        int current_count = await CountExistingEvents();
        if (current_count >= LIMIT)
            return 0;

        // (demo) Run the embedded seed script to create table from scratch:
        string tablequery = embeds.GetFileContents<CalendarEventService>("SEED_CalendarEvents.sql").Dump();
        int count_from_script = await CreateConnection().ExecuteAsync(tablequery);

        // (demo) Create fake events using embedded sql and C#
        var fake_events = CreateFakeEvents(300).ToArray();
        var count_from_fakes = await Create("create_calendar_event.sql", fake_events);

        int total_count = count_from_fakes; // + count_from_script;
        Console.WriteLine($"{total_count} row(s) inserted.");
        return total_count;
    }

    private static string[] fake_event_names = new string[]
        { "Eat Dinner", "Build the Taj Mahal", "Reinvent the wheel", "Found SpaceX" };

    private static string[] fake_descriptions = new string[]
    {
        "Eat Dinner with the queen", "Build the Taj Mahal in one day",
        "Reinvent the wheel and build a time machine set to the stone age", "Found SpaceX before Elon Musk does..."
    };

    private static int[] days = Enumerable.Range(-30, 30).ToArray();

    // Any validation logic can go here. 
    // I'm just showing off NSpecification because it can do LINQ and compound specs (using & and |, see: https://github.com/ASbeletsky/NSpecifications)
    private Spec<CalendarEvent> has_valid_id = new Spec<CalendarEvent>(e => e.id > -1);

    private string
        connectionString =
            "Data Source=LocalDatabase.db"; // TODO: Obviously, we want to read this from .env in the future.

    private List<CalendarEvent> CreateFakeEvents(int count = 3)
    {
        var index = 1;

        var calendar_faker = new Faker<CalendarEvent>()
                .CustomInstantiator(f => new CalendarEvent())
                .RuleFor(o => o.last_modified, f => f.Date.Recent(100))
                .RuleFor(o => o.start_date, f => f.Date.Recent(365)
                    // .Add(TimeSpan.FromDays(days.TakeFirstRandom()))
                )
                .RuleFor(o => o.event_name, f => fake_event_names.TakeFirstRandom())
                .RuleFor(o => o.description, f => fake_descriptions.TakeFirstRandom())
            ;

        var items = calendar_faker.Generate(count);
        Console.WriteLine($"created {items.Count} fake events   ");
        // items.Dump("faked");
        return items;
    }

    public async Task<IEnumerable<CalendarEvent>> GetAll()
    {
        string sql = embeds.GetFileContents<CalendarEventService>("get_all_calendar_events.sql");
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

        return events_found.SingleOrDefault();
    }

    public Task<List<CalendarEvent>> Search(Part search)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Create(string sql_file_path, params CalendarEvent[] records)
    {
        records.Select(x => x.start_date).Dump("dates");


        string sql = embeds.GetFileContents<CalendarEventService>(sql_file_path);
        Console.WriteLine($"creating {records.Length} records...");
        // records.Dump("creating parts");
        // https://regex101.com/r/XyhgkI/1connection
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
                , (builder, calendarEvent) =>
                {
                    builder.Append("(");
                    builder.Append($" '{calendarEvent.event_name}', ");
                    builder.Append($" '{calendarEvent.description}', ");
                    builder.Append($" '{calendarEvent.start_date.ToString("yyyy/MM/dd")}'");
                    builder.AppendLine("),");
                    return builder;
                })
            .RemoveFromEnd(2)
            .ToString();

        Console.WriteLine($"bulk sql for {records.Length} records :>> " + bulk_sql);

        // var conn = CreateConnection();
        await using SQLiteConnection connection = new SQLiteConnection(connectionString);
        connection.Open();

        await using (SQLiteCommand cmd = connection.CreateCommand())
        {
            cmd.CommandText = bulk_sql;
            cmd.CommandType = CommandType.Text;
            var record_count = await cmd.ExecuteNonQueryAsync();
            // var record_count = await connection.ExecuteSqlAsync(bulk_sql, records);
            Console.WriteLine("RECORDS CREATED : " + record_count);
            return record_count;
        }
    }

    public Task Update(int id, CalendarEvent model)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        var events_found = await GetById(id);
        bool valid_event = events_found.Is(has_valid_id);

        var connection = CreateConnection();

        // Perform the actual deletion
        if (valid_event)
        {
            var result =
                await connection.ExecuteAsync("delete from CalendarEvents where id = @id", new { id = id });
            // return result;
        }
    }
}