using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contracts.Autorization;
using Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorizationController : BaseController
{
    private readonly IConfiguration configuration;
    private readonly ILogger<AuthorizationController> logger;
    private readonly JwtIssuerOptions jwtIssuerOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    public AuthorizationController(IConfiguration configuration, ILogger<AuthorizationController> logger, IOptions<JwtIssuerOptions> jwtIssuerOptions)
    {
        this.configuration = configuration;
        this.logger = logger;
        this.jwtIssuerOptions = jwtIssuerOptions.Value;
    }

    /// <summary>
    /// Validates User's LogonName and password, then returns their permissions/claims
    /// </summary>
    /// <param name="request">LogonName and password POST payload.</param>
    /// <response code="200">logon successful.</response>
    ///	<response code="400">Invalid LogonName or password format</response>
    ///	<response code="401">Invalid LogonName or password.</response>
    /// <returns>Returns JavaScript Web Token(JWT) + expiry time.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Logon([FromBody] LoginRequestDto request)
    {
        var claims = new List<Claim>();

        //PersonAuthenticateLogonDetailsDto userLogonUserDetails;
        IEnumerable<string> permissions;

        // using (SqlConnection db = new SqlConnection(configuration.GetConnectionString("WorldWideImportersDatabase")))
        // {
        //     userLogonUserDetails = await db.QuerySingleOrDefaultWithRetryAsync<PersonAuthenticateLogonDetailsDto>("[Website].[PersonAuthenticateLookupByLogonNameV2]",
        //         param: request, commandType: CommandType.StoredProcedure);
        //     if (userLogonUserDetails == null)
        //     {
        //         logger.LogWarning("Login attempt by user {LogonName} failed", request.LogonName);
        //
        //         return this.Unauthorized();
        //     }
        //
        //     // Lookup the Person's permissions
        //     // permissions = await db.QueryWithRetryAsync<string>("[Website].[PersonPermissionsByPersonIdV1]", new { userLogonUserDetails.PersonID },
        //     //     commandType: CommandType.StoredProcedure);
        // }

        // Setup the primary SID + name info
        // claims.Add(new Claim(ClaimTypes.PrimarySid, userLogonUserDetails.PersonID.ToString()));
        // if (userLogonUserDetails.IsSystemUser)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, "SystemUser"));
        // }
        //
        // if (userLogonUserDetails.IsEmployee)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, "Employee"));
        // }
        //
        // if (userLogonUserDetails.IsSalesPerson)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, "SalesPerson"));
        // }

        // foreach (string permission in permissions)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, permission));
        // }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtIssuerOptions.SecretKey));

        var token = new JwtSecurityToken(
            issuer: jwtIssuerOptions.Issuer,
            audience: jwtIssuerOptions.Audience,
            expires: DateTime.UtcNow.Add(jwtIssuerOptions.TokenExpiresAfter),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
        });
    }
}