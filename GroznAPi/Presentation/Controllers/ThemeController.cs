using System.ComponentModel.DataAnnotations;
using Contracts.Theme;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repository.Abstractions;
using Service;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ThemeController : BaseController
{
    private readonly ILogger<TestController> _logger;
    private readonly ThemeService _themeService;

    public ThemeController(ILogger<TestController> logger, ThemeService themeService)
    {
        _logger = logger;
        _themeService = themeService;
    }
    
    [HttpGet("theme/get/{themeId}")]
    public async Task<ActionResult<GetThemeResponseDto>> GetUserAsync([Required] GetThemeRequestDto req)
    {
        if (req.themeId == 0)
        {
            return BadRequest("Invalid id");
        }
        var resp = await _themeService.GetTheme(req.themeId);
        return Ok(resp);
    }

    [HttpGet("theme/get/lessons")]
    public async Task<ActionResult<GetLessonsResponseDto>> GetLessons()
    {
        var resp = await _themeService.GetLessons();
        return Ok(resp);
    }

    [HttpPost("theme/create")]
    public async Task<ActionResult<CreateThemeResponseDto>> CreateTheme([Required] CreateThemeRequestDto req)
    {
        var resp = await _themeService.CreateTheme(req);
        return Ok(resp);
    }

    [HttpDelete("theme/delete")]
    public async Task DeleteTheme([Required] DeleteThemeRequestDto req)
    {
        await _themeService.DeleteTheme(req);
    }

    [HttpPost("theme/update")]
    public async Task<ActionResult<UpdateThemeResponseDto>> UpdateTheme([Required] UpdateThemeRequestDto req)
    {
       return await _themeService.UpdateTheme(req);
    }
    
}