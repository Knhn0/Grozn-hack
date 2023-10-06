using Contracts.Course;
using Domain.Entities;
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
    private readonly IAccountService _accountService;

    public CourseController(ILogger<CourseController> logger, ICourseService courseService, IAccountService accountService)
    {
        _logger = logger;
        _courseService = courseService;
        _accountService = accountService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Course>>> GetAllCourses()
    {
        return Ok(await _courseService.GetAllCoursesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourseById([FromRoute] int id)
    {
        return Ok(await _courseService.GetByIdAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<List<Course>>> GetMyCourses()
    {
        if (Role == "admin") return Ok(await GetAllCourses());
        return Ok(await _courseService.GetJoinedCourses(AccountId));
    }

    [HttpPost]
    public async Task<ActionResult<CourseCreatedResponseDto>> CreateCourse(CreateCourseRequestDto request)
    {
        var userInfo = await _accountService.GetByIdAsync(AccountId);
        return Ok(await _courseService.CreateCourseAsync(request, userInfo!.Id));
    }

    [HttpDelete]
    public async Task<ActionResult<CourseRemovedResponseDto>> DeleteCourse(RemoveCourseRequestDto request)
    {
        if (Role == "Admin") return await _courseService.RemoveCourseForcedAsync(request);
        if (Role == "Teacher")
        {
            var userInfo = await _accountService.GetByIdAsync(AccountId);
            return await _courseService.RemoveCourseAsync(request, userInfo.Id);
        }

        return BadRequest("Invalid");
    }
}