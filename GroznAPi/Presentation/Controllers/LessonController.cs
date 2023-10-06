using Contracts.Lesson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    [HttpGet("/all")]
    public async Task<ActionResult<GetLessonsResponseDto>> GetLessons()
    {
        var resp = await _lessonService.GetAllAsync();
        return Ok(resp);
    }

    [HttpDelete("/{id}")]
    public async Task<ActionResult> DeleteLesson(int id)
    {
        if (Role == "Student") return BadRequest("Invalid account type");
        await _lessonService.DeleteLesson(id);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<UpdateLessonResponseDto>> UpdateLesson([FromBody] UpdateLessonRequestDto req)
    {
        if (Role == "Student") return BadRequest("Invalid account type");
        return await _lessonService.UpdateLesson(req);
    }
}