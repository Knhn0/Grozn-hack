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
public class LessonController : BaseController
{
    private readonly ILogger<LessonController> _logger;
    private readonly ILessonService _lessonService;


    public LessonController(ILogger<LessonController> logger, ILessonService lessonService)
    {
        _logger = logger;
        _lessonService = lessonService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateLesson([FromBody] CreateLessonRequestDto req)
    {
        if (Role == "Student") return BadRequest("Invalid account type");
        var lesson =  await _lessonService.CreateLesson(req);
        return Ok(lesson);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetLessonAsync(int id)
    {
        if (id == 0)
        {
            return BadRequest("Invalid id");
        }

        var resp = await _lessonService.GetLesson(id);
        return Ok(resp);
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetLessons()
    {
        var resp = await _lessonService.GetAllAsync();
        return Ok(resp);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLesson(int id)
    {
        if (Role == "Student") return BadRequest("Invalid account type");
        await _lessonService.DeleteLesson(id);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateLesson([FromBody] UpdateLessonRequestDto req)
    {
        if (Role == "Student") return BadRequest("Invalid account type");
        var resp = await _lessonService.UpdateLesson(req);
        return Ok(resp);
    }
    
    [HttpPost("getPercents")]
    public async Task<ActionResult<GetLessonsPercentResponseDto>> GetLessonsPercentByThemeId([FromBody] GetLessonsPercentRequest req)
    {
        return await _lessonService.GetLessonsPercentByThemeId(req.ThemeId, req.StudentId);
    }

}