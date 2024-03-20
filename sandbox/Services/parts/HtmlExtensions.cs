using Newtonsoft.Json;

namespace Orchard.Sandbox;

public static class HtmlExtensions
{
    public static string AsJS<T>(this T obj) => JsonConvert.SerializeObject(obj);
}