using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Orchard.Sandbox.Services;

/// <summary>
/// This sample Service may be @injected anywhere in any Orchardcore UI module or API.
/// See README.md for explanation.
/// </summary>
public class GunService : INugsService
{
    private readonly EmbeddedResourceService embeds;

    public GunService(IEmbeddedResourceQuery embed_service)
    {
        // set up the service to read local embedded files, like .md or .sql, etc.
        embeds = (EmbeddedResourceService?)embed_service;

        // toggles whether we want to log existing tables to console, which I'm using to verify that the sqlite db has my tables.  Also, it was FUN. :)
        bool find_tables = Environment
            .GetEnvironmentVariable("FIND_TABLES")
            .ToBoolean(fallback: true);

        if (find_tables) FindTables();
    }

    public List<string> FindTables()
    {
        using var connection = CreateConnection();
        string query = embeds.GetFileContents<GunService>("find_all_tablenames.sql");

        var tables = connection.Query<string>(query);
        var tableNames = tables.Dump("tables found");
        return tables.ToList();
    }

    public async Task<IEnumerable<Part>> GetAll()
    {
        // string sql = embeds.GetFileContents<GunService>("get_all_calendar_events.sql").Dump();
        string sql = """SELECT * FROM parts""";
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<Part>(sql);
        // records.Dump("records");
        return records;
    }

    public async Task<List<Part>> Search(Part search)
    {
        string sql = embeds.GetFileContents<GunService>("search_parts.sql");
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
        string sql = embeds.GetFileContents<GunService>("create_part.sql");
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
        string sql = embeds.GetFileContents<GunService>("update_part.sql");
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