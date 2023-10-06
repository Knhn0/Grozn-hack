using Contracts.Course;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        if (Role == "Admin") return Ok(await GetAllCourses());
        return Ok(await _courseService.GetJoinedCourses(AccountId));
    }

    [HttpPost]
    public async Task<ActionResult<CreateCourseResponseDto>> CreateCourse(CreateCourseRequestDto request)
    {
        var userInfo = await _accountService.GetByIdAsync(AccountId);
        return Ok(await _courseService.CreateCourseAsync(request, userInfo!.Id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        switch (Role)
        {
            case "Admin":
                await _courseService.RemoveCourseForcedAsync(id);
                return Ok();
            case "Teacher":
            {
                var userInfo = await _accountService.GetByIdAsync(AccountId);
                await _courseService.RemoveCourseAsync(id, userInfo.Id);
                return Ok();
            }
            default:
                return BadRequest("Invalid");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCourse(UpdateCourseRequestDto request)
    {
        switch (Role)
        {
            case "Admin":
                var resp = await _courseService.UpdateCourseForcedAsync(request);
                return Ok(resp);
            case "Teacher":
            {
                var userInfo = await _accountService.GetByIdAsync(AccountId);
                var respUser = await _courseService.UpdateCourseAsync(request, userInfo.Id);
                return Ok(respUser);
            }
            default:
                return BadRequest("Invalid");
        }
    }
}