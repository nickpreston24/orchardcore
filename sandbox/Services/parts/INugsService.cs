using System.Data;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Orchard.Sandbox.Services;

public interface INugsService
{
    Task<IEnumerable<Part>> GetAll();
    Task<List<Part>> Search(Part search);
    Task<Part> GetById(int id);
    Task<int> Create(params Part[] model);
    Task Update(int id, Part model);
    Task Delete(int id);
}

public class NugsService : INugsService
{
    private readonly EmbeddedResourceService embeds;

    public NugsService(IEmbeddedResourceQuery embed_service)
    {
        // set up the service to read local embedded files, like .md or .sql, etc.
        embeds = (EmbeddedResourceService?)embed_service;

        // toggles whether we want to log existing tables to console, which I'm using to verify that the sqlite db has my tables.  Also, it was FUN. :)
        bool find_tables = Environment
            .GetEnvironmentVariable("FIND_TABLES")
            .ToBoolean(fallback: true);

        if (find_tables) FindTables();
    }

    private IEnumerable<string> FindTables()
    {
        using var connection = CreateConnection();
        string query = embeds.GetFileContents<NugsService>("find_all_tablenames.sql");

        var tables = connection.Query<string>(query);
        var tableNames = tables.Dump("tables found");
        return tables;
    }

    public async Task<IEnumerable<Part>> GetAll()
    {
        // string sql = embeds.GetFileContents<NugsService>("get_all_calendar_events.sql").Dump();
        string sql = """SELECT * FROM parts""";
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<Part>(sql);
        // records.Dump("records");
        return records;
    }

    public async Task<List<Part>> Search(Part search)
    {
        string sql = embeds.GetFileContents<NugsService>("search_parts.sql");
        // search.Dump(nameof(search));
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<Part>(sql, search);
        return records.ToList();
    }

    private SqliteConnection CreateConnection() => new SqliteConnection("Data Source=Nugs.db");

    public Task<Part> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Create(params Part[] model)
    {
        string sql = embeds.GetFileContents<NugsService>("create_part.sql");
        // model.Dump("creating parts");
        Console.WriteLine(sql.AsMultiInsert(10));
        // using var connection = CreateConnection();
        //
        // var records = await connection.ExecuteAsync(sql, model);
        // return records;
        return default;
    }

    public async Task Update(int id, Part model)
    {
        string sql = embeds.GetFileContents<NugsService>("update_part.sql");
        // Console.WriteLine(sql);
        // search.Dump();
        using var connection = CreateConnection();
        var records = await connection.ExecuteAsync(sql, new
        {
            url = model.url, name = model.name, imageurl = model.imageurl, kind = model.kind
        });

        Console.WriteLine($"Records changed: {records}");
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}

public class Part
{
    public int id { get; set; } = -1;
    public string name { get; set; } = string.Empty;
    public string manufacturer { get; set; } = string.Empty;
    public double cost { get; set; }
    public string kind { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
    public string imageurl { get; set; } = string.Empty;
}

// This sample Service may be @injected anywhere in any Orchardcore UI module or API.
// See README.md for explanation.
public class SampleMarkdownService
{
    private readonly EmbeddedResourceService embedsvc;

    public SampleMarkdownService(IEmbeddedResourceQuery embeds)
    {
        string local_readme = "README.md";
        embedsvc =
            (EmbeddedResourceService?)embeds; // the embedded resource service magically reads our embedded files defind in our .csproj.

        var all_markdown_headers = new Grepper() { FileSearchMask = "*.md", FileSearchLinePattern = "^#*" }
                .GetMatchingFiles() // returns IEnumerable<GrepResult>
                .Dump("debug") //  dumps as JSON to console, then returns T as-is. great for debugging in prod.
                .Select(file => File.ReadAllText(file.FilePath)) // Read all text (duh)
                .Select(text => text.Extract<MarkdownHeader>(MarkdownHeader.HeaderPattern))
            ;
    }
}

public record MarkdownHeader
{
    public string RawLine { get; set; } = string.Empty; // the entire matched line
    public string Header { get; set; } = string.Empty; // entire matched header

    public int HeaderLength =>
        Header.NotEmpty() ? Header.Length : 0; // easy computed property for UI or whatever we want.

    // We can refactor this into a flyweight pattern, or whatever.   :)
    public static Regex HeaderPattern = new Regex("", RegexOptions.Compiled | RegexOptions.Multiline);
}