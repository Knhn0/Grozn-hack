using Contracts.Registraton;
using Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Abstactions;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class RegistrationController : BaseController
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<RegistrationController> _logger;
    private readonly JwtIssuerOptions _jwtIssuerOptions;

    private readonly IAuthorizationService _authorizationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    public RegistrationController(IConfiguration configuration, ILogger<RegistrationController> logger, IOptions<JwtIssuerOptions> jwtIssuerOptions,
        IAuthorizationService authorizationService)
    {
        this._configuration = configuration;
        this._logger = logger;
        this._authorizationService = authorizationService;
        this._jwtIssuerOptions = jwtIssuerOptions.Value;
    }

    /// <summary>
    ///  Registers a new user
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
    {
        var response = await _authorizationService.RegisterAsync(request);
        return Ok(response);
    }
}