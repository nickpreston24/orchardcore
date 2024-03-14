using System.Data;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Orchard.Sandbox.Services;

public interface INugsService
{
    Task<IEnumerable<Part>> GetAll();
    Task<List<Part>> Search(Part search);
    Task<Part> GetById(int id);
    Task Create(Part model);
    Task Update(int id, Part model);
    Task Delete(int id);
}

public class NugsService : INugsService
{
    private readonly EmbeddedResourceService embeds;

    public NugsService(IEmbeddedResourceQuery embed_service)
    {
        embeds = (EmbeddedResourceService?)embed_service;
        // FindTables();
    }

    private void FindTables()
    {
        using var connection = CreateConnection();

        var tables = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table'");
        var tableNames = tables.Dump("tables found");
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

    public async Task Create(Part model)
    {
        string sql = embeds.GetFileContents<NugsService>("create_part.sql");
        model.Dump("creating part");
        using var connection = CreateConnection();

        var records = await connection.ExecuteAsync(sql, model);
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
    public string imageurl { get; set; } = "wwwroot/img/pewpewlogo.jpg";
}