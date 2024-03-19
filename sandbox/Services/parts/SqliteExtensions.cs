using System.Reflection;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Diagnostics;

namespace Orchard.Sandbox.Services;

public static class SqliteExtensions
{
    private static readonly RegexOptions options =
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

    // https://regex101.com/r/NaKq56/1
    private static Regex insert_values_clause =
        new Regex(@"(?<values_expression>values\s*\((?<raw_variables>[@\w\s,?]+)\);?)", options);

    // 
    private static Regex hydrate_insert_values = new Regex(@"", options);

    public static string AsMultiInsert(this string query, int limit = 1)
    {
        int max_limit = 1000; // set with whatever.
        
        if (limit > max_limit)
            throw new ArgumentOutOfRangeException(limit + $" may not be more than {max_limit}");

        string update = query.Repeat(insert_values_clause);
        return update;
    }

    public static string Repeat(this string query, Regex repeat_pattern)
    {
        Console.WriteLine($"before: '{query}'");

        var valuestrings = query.Extract<ValuesSql>(repeat_pattern).FirstOrDefault();
        valuestrings.Dump(nameof(valuestrings));


        string updated_query =
            Regex.Replace(query, valuestrings.values_expression, "valuesgohear");

        Console.WriteLine($"after '{updated_query}'");

        return updated_query;
    }

    public class ValuesSql
    {
        public string values_expression { get; set; } = string.Empty;
        public string raw_variables { get; set; } = string.Empty;
    }

    private static readonly IDictionary<Type, ICollection<PropertyInfo>> _propertyCache =
        new Dictionary<Type, ICollection<PropertyInfo>>();


    public static string Hydrate<T>(this string query, Regex hydrate_pattern)
    {
        var props = _propertyCache.TryGetProperties<T>();
        var prop_names = props.Select(x => x.Name).ToArray();

        var value_vars = Regex.Split(@",", query);
        value_vars.Dump(nameof(value_vars));

        return query;
    }
}