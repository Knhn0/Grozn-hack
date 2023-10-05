using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;


[Route("[controller]")]
[ApiController]
public class LessonController: BaseController
{
    private readonly ILogger<LessonController> _logger;

    public LessonController(ILogger<LessonController> logger)
    {
        _logger = logger;
    }
}