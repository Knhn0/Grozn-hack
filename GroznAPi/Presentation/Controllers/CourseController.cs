using Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Abstactions;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class CourseController : BaseController
{
    private readonly ILogger<CourseController> _logger;
    private readonly ICourseService _courseService;

    public CourseController(ILogger<CourseController> logger, ICourseService courseService)
    {
        _logger = logger;
        _courseService = courseService;
    }
    
    
}