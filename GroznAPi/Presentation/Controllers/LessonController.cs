﻿using System.ComponentModel.DataAnnotations;
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


    [HttpGet("lesson/get/{lessonId}")]
    public async Task<ActionResult<GetLessonsResponseDto>> GetLessonAsync([Required] GetLessonRequestDto req)
    {
        if (req.LessonId == 0)
        {
            return BadRequest("Invalid id");
        }

        var resp = await _lessonService.GetLesson(req);
        return Ok(resp);
    }

    [HttpGet("lessons/getTheme/{lessonId}")]
    public async Task<ActionResult<GetLessonThemeResponseDto>> GetLessonTheme(GetLessonThemeRequestDto req)
    {
        if (req.LessonId == 0)
        {
            return BadRequest("Invalid id");
        }
        var resp = await _lessonService.GetTheme(req);
        return Ok(resp);
    }

    [HttpGet("lessons/get")]
    public async Task<ActionResult<GetLessonsResponseDto>> GetLessons()
    {
        var resp = await _lessonService.GetAllAsync();
        return Ok(resp);
    }

    [HttpDelete("lesson/delete")]
    public async Task<ActionResult<DeleteLessonResponseDto>> DeleteLesson(DeleteLessonRequestDto req)
    {
        await _lessonService.DeleteLesson(req);
        return Ok(req);
    }

    [HttpPut("lesson/update")]
    public async Task<ActionResult<UpdateLessonResponseDto>> Updatelesson(UpdateLessonRequestDto req)
    {
        return await _lessonService.UpdateLesson(req);
        
    }

    [HttpPost("lesson/getPercents")]
    public async Task<ActionResult<GetLessonsPercentResponseDto>> GetLEssonsPercentByThemeId([FromBody] GetLessonsPercentRequest req)
    {
        return await _lessonService.GetLessonsPercentByThemeId(req.ThemeId, req.StudentId);
    }
    
}