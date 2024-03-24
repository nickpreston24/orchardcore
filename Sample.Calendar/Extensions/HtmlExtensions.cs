using Newtonsoft.Json;

namespace Sample.Calendar;

public static class HtmlExtensions
{
    public static string AsJS<T>(this T obj) => JsonConvert.SerializeObject(obj);
}