using System.Text.Json;

namespace Helpers;

public static class Common
{
    public static T FromJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}