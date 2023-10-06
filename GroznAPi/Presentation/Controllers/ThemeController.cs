using System.ComponentModel.DataAnnotations;
using Contracts.Theme;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repository.Abstractions;
using Service;
using Service.Abstactions;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ThemeController : BaseController
{
    private readonly ILogger<TestController> _logger;
    private readonly IThemeService _themeService;

    public ThemeController(ILogger<TestController> logger, IThemeService themeService)
    {
        _logger = logger;
        _themeService = themeService;
    }
    
    [HttpGet("/{themeId}")]
    public async Task<ActionResult<GetThemeResponseDto>> GetUserAsync([Required] GetThemeRequestDto req)
    {
        if (req.ThemeId == 0)
        {
            return BadRequest("Invalid id");
        }
        var resp = await _themeService.GetThemeAsync(req.ThemeId);
        return Ok(resp);
    }

    [HttpGet]
    public async Task<ActionResult<GetLessonsResponseDto>> GetLessons()
    {
        var resp = await _themeService.GetLessonsAsync();
        return Ok(resp);
    }

    [HttpPost]
    public async Task<ActionResult<CreateThemeResponseDto>> CreateTheme([Required] CreateThemeRequestDto req)
    {
        var resp = await _themeService.CreateThemeAsync(req);
        return Ok(resp);
    }

    [HttpDelete]
    public async Task DeleteTheme([Required] DeleteThemeRequestDto req)
    {
        await _themeService.DeleteThemeAsync(req);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateThemeResponseDto>> UpdateTheme([Required] UpdateThemeRequestDto req)
    {
       return await _themeService.UpdateThemeAsync(req);
    }

    [HttpGet("by-course/{courseId}")]
    public async Task<ActionResult<List<ThemeDto>>> GetByCourseId([FromQuery] int courseId)
    {
        return await _themeService.GetThemesByCourseId(courseId);
    }
}