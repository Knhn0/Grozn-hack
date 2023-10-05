using Jwt;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected int AccountId => JwtParser.GetAccountId(AuthHeader);
    protected string Role => JwtParser.GetRole(AuthHeader);
    protected string AuthHeader => HttpContext.Request.Headers["Authorization"].ToString();
}