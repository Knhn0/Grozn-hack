using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Jwt;

public static class JwtParser
{
    public static string GetRole(this string token)
    {
        return ParserToken(token, "role");
    }

    public static int GetAccountId(this string token)
    {
        return int.Parse(ParserToken(token, "userdata"));
    }

    private static string ParserToken(this string token, string role)
    {
        var removeBearer = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var tokenData = handler.ReadJwtToken(removeBearer);
        var s = tokenData.Payload;
        var t = s.Claims.FirstOrDefault(c => c.Type.Split('/').Last() == role).Value;
        return t;
    }
}