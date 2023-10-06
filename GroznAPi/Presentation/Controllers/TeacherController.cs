using System.ComponentModel.DataAnnotations;
using Contracts.Teacher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Abstactions;

namespace Presentation.Controllers;

public class TeacherController : BaseController
{
    private readonly ILogger<LessonController> _logger;
    private readonly ITeacherService _teacherService;

    public TeacherController(ILogger<LessonController> logger, ITeacherService teacherService)
    {
        _logger = logger;
        _teacherService = teacherService;
    }

    [HttpGet("/{teacherId}")]
    public async Task<ActionResult<GetTeacherDto>> GetTeacher([Required] int id)
    {
        if (id == 0)
        {
            return BadRequest("Id is not valid");
        }

        var resp = await _teacherService.GetTeacher(id);
        return Ok(resp);
    }

    [HttpGet("/get")]
    public async Task<ActionResult<GetTeacherDto>> GetAllTeachers()
    {
        var resp = await _teacherService.GetTeachers();
        return Ok(resp);
    }

    [HttpPut("/update")]
    public async Task<ActionResult<UpdateTeacherResponseDto>> UpdateTeacher(UpdateTeacherRequestDto req)
    {
        var resp = await _teacherService.UpdateTeacher(req);
        return Ok(resp);
    }

    [HttpPost("/create")]
    public async Task<ActionResult<CreateTeacherResponseDto>> CreateTeacher(CreateTeacherRequestDto req)
    {
        var resp = await _teacherService.CreateTeacher(req);
        return Ok(resp);
    }

    [HttpDelete("/delete")]
    public async Task<ActionResult<bool>> DeleteTeacher(DeleteTeacherRequestDto req)
    {
        var resp = await _teacherService.DeleteTeacher(req);
        return Ok(resp);
    }

    [HttpGet("/get/{teacherId}")]
    public async Task<ActionResult<GetTeacherInfoDto>> GetTeacherUserInfo([Required] int id)
    {
        if (id == 0)
        {
            return BadRequest("Id is not valid");
        }

        var resp = await _teacherService.GetUserInfo(id);
        return Ok(resp);
    }

    [HttpGet("/get/createdCourses/{teacherId}")]
    public async Task<ActionResult<GetCreatedCourseDto>> GetCreatedCources([Required] int id)
    {
        if (id == 0)
        {
            return BadRequest("Id is not valid");
        }

        var resp = await _teacherService.GetCreatedCourses(id);
        return Ok(resp);
    }

    [HttpGet("/isTeacher")]
    public async Task<ActionResult<bool>> IsTeacher(int id)
    {
        if (id == 0)
        {
            return BadRequest("Id is not valid");
        }

        var resp = await _teacherService.IsTeacher(id);
        return Ok(resp);
    }
    
    
}
