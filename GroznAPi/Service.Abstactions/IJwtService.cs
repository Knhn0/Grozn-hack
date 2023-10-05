using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jwt;

namespace Service.Abstactions;

public interface IJwtService
{
    JwtSecurityToken CreateJwtToken(ICollection<Claim> claims, int tokenExpiresAfterHours = 0);
}