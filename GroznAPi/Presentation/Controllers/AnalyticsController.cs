using Contracts.Analitic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class AnalyticsController
{
    private readonly ILogger<AnalyticsController> _logger;
    private readonly LessonService _lessonService;

    public AnalyticsController(ILogger<AnalyticsController> logger, LessonService lessonService)
    {
        _logger = logger;
        _lessonService = lessonService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("theme/balls")]
    public async Task<ThemeResponseDto> GetTestByStudentIdAndTestId([FromBody] ThemeRequestSiDandTidDto body)
    {
        return await _lessonService.GetAllTestsByThemeId(body.ThemeId, body.StudentId);
    }
}