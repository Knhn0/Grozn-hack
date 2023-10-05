using Contracts.Registraton;
using Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class RegistrationController : BaseController
{
    private readonly IConfiguration configuration;
    private readonly ILogger<RegistrationController> logger;
    private readonly JwtIssuerOptions jwtIssuerOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    public RegistrationController(IConfiguration configuration, ILogger<RegistrationController> logger, IOptions<JwtIssuerOptions> jwtIssuerOptions)
    {
        this.configuration = configuration;
        this.logger = logger;
        this.jwtIssuerOptions = jwtIssuerOptions.Value;
    }

    /// <summary>
    ///  Registers a new user
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegistrationRequestDto request)
    {
        return Ok(new RegistrationResponseDto());
    }
}