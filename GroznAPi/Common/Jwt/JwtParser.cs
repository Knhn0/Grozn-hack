using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Jwt;

public class JwtParser
{
    public static string GetRole(string jwtInput)
    {
        return GetParameter<string>(jwtInput, nameof(ClaimTypes.Role));
    }
    
    public static int GetAccountId(string jwtInput)
    {
        return GetParameter<int>(jwtInput, nameof(ClaimTypes.UserData));
    }


    private static T GetParameter<T>(string jwtInput, string parameterKey)
    {
        if (string.IsNullOrWhiteSpace(jwtInput))
        {
            return default(T);
        }

        var removeBearer = jwtInput.Split(' ')[1];
        var getPayload = removeBearer.Split('.')[1];
        var base64EncodedBytes = Convert.FromBase64String(getPayload);
        var parsedPayload = Encoding.UTF8.GetString(base64EncodedBytes);
        var jObject = JObject.Parse(parsedPayload);
        return jObject[parameterKey] != null ? jObject[parameterKey].ToObject<T>() : default(T);
    }
}