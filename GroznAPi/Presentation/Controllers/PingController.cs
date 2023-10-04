using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class PingController : BaseController
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PingController> _logger;


    public PingController(IConfiguration configuration, ILogger<PingController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Login()
    {
        _logger.LogInformation("Ping");
        return Ok("pong");
    }

    [HttpGet("authorize")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> LoginAuthorize()
    {
        _logger.LogInformation("Ping authorize");
        return Ok("pong authorize");
    }
}