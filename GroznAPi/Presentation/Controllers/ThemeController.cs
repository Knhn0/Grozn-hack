using System.ComponentModel.DataAnnotations;
using Contracts.Theme;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Abstactions;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ThemeController : BaseController
{
    private readonly ILogger<ThemeController> _logger;
    private readonly IThemeService _themeService;

    public ThemeController(ILogger<ThemeController> logger, IThemeService themeService)
    {
        _logger = logger;
        _themeService = themeService;
    }

    [HttpGet("{themeId}")]
    public async Task<ActionResult> GetUserAsync(int themeId)
    {
        if (themeId == 0)
        {
            return BadRequest("Invalid id");
        }

        var resp = await _themeService.GetThemeAsync(themeId);
        return Ok(resp);
    }

    [HttpPost]
    public async Task<ActionResult<CreateThemeResponseDto>> CreateTheme([Required] CreateThemeRequestDto req)
    {
        var resp = await _themeService.CreateThemeAsync(req);
        return Ok(resp);
    }

    [HttpDelete("{id}")]
    public async Task DeleteTheme(int id)
    {
        await _themeService.DeleteThemeAsync(id);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateThemeResponseDto>> UpdateTheme([Required] UpdateThemeRequestDto req)
    {
        return await _themeService.UpdateThemeAsync(req);
    }

    [HttpGet("by-course/{courseId}")]
    public async Task<ActionResult<List<ThemeDto>>> GetByCourseId([FromRoute] int courseId)
    {
        return await _themeService.GetThemesByCourseId(courseId);
    }
}