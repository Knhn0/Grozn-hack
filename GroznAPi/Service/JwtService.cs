using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Helpers;
using Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Abstactions;

namespace Service;

public class JwtService : IJwtService
{
    private readonly JwtIssuerOptions _jwtIssuerOptions;

    public JwtService(IConfiguration configuration)
    {
        var s = configuration["JwtIssuerOptions:TokenExpiresAfter"];
        _jwtIssuerOptions = new JwtIssuerOptions
        {
            Issuer = configuration["JwtIssuerOptions:Issuer"],
            Audience = configuration["JwtIssuerOptions:Audience"],
            SecretKey = configuration["JwtIssuerOptions:SecretKey"],
            TokenExpiresAfterHours = configuration["JwtIssuerOptions:TokenExpiresAfterHours"].FromJson<int>()
        };
    }


    public JwtSecurityToken CreateJwtToken(ICollection<Claim> claims, int tokenExpiresAfterHours = 0)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtIssuerOptions.SecretKey));
        if (tokenExpiresAfterHours == 0)
        {
            tokenExpiresAfterHours = _jwtIssuerOptions.TokenExpiresAfterHours;
        }

        var token = new JwtSecurityToken(
            issuer: _jwtIssuerOptions.Issuer,
            audience: _jwtIssuerOptions.Audience,
            expires: DateTime.UtcNow.AddHours(tokenExpiresAfterHours),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
}