using System.ComponentModel.DataAnnotations;
using Contracts.Lesson;
using Contracts.Theme;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Abstractions;
using Service.Abstactions;

namespace Presentation.Controllers;


[Route("[controller]")]
[ApiController]
public class LessonController: BaseController
{
    private readonly ILogger<LessonController> _logger;
    private readonly ILessonService _lessonService;
    

    public LessonController(ILogger<LessonController> logger, ILessonService lessonService)
    {
        _logger = logger;
        _lessonService = lessonService;
    }


    [HttpGet("/get/{lessonId}")]
    public async Task<ActionResult<GetLessonsResponseDto>> GetLessonAsync([Required] GetLessonRequestDto req)
    {
        if (req.LessonId == 0)
        {
            return BadRequest("Invalid id");
        }

        var resp = await _lessonService.GetLesson(req);
        return Ok(resp);
    }

    [HttpGet("/getTheme/{lessonId}")]
    public async Task<ActionResult<GetLessonThemeResponseDto>> GetLessonTheme(GetLessonThemeRequestDto req)
    {
        if (req.LessonId == 0)
        {
            return BadRequest("Invalid id");
        }
        var resp = await _lessonService.GetTheme(req);
        return Ok(resp);
    }

    [HttpGet("/get")]
    public async Task<ActionResult<GetLessonsResponseDto>> GetLessons()
    {
        var resp = await _lessonService.GetAllAsync();
        return Ok(resp);
    }

    [HttpDelete("/delete")]
    public async Task<ActionResult<DeleteLessonResponseDto>> DeleteLesson(DeleteLessonRequestDto req)
    {
        if (Role == "Student") return BadRequest("Invalid account type");
        await _lessonService.DeleteLesson(req);
        return Ok(req);
    }

    [HttpPut("/update")]
    public async Task<ActionResult<UpdateLessonResponseDto>> Updatelesson(UpdateLessonRequestDto req)
    {
        if (Role == "Student") return BadRequest("Invalid account type");
        return await _lessonService.UpdateLesson(req);
    }
    
}