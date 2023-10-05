using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ThemeController : BaseController
{
    private readonly ILogger<TestController> _logger;

    public ThemeController(ILogger<TestController> logger)
    {
        _logger = logger;
    }
}