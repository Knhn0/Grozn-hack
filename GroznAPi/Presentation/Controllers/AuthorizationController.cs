using Contracts.Autorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstactions;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorizationController : BaseController
{
    private readonly IAuthorizationService _authorizationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
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
    public async Task<ActionResult> Login([FromBody] LoginRequestDto request)
    {
        return Ok(await _authorizationService.Login(request));
    }
}