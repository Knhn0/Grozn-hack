using Contracts.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Abstactions;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class TestController : BaseController
{
    private readonly ILogger<TestController> _logger;
    private readonly ITestService _testService;

    public TestController(ILogger<TestController> logger, ITestService testService)
    {
        _logger = logger;
        _testService = testService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<TestDto>>> GetAllTests()
    {
        return await _testService.GetAllTestsAsync();
    }

    [HttpGet("by-lesson/{id}")]
    public async Task<ActionResult<List<TestDto>>> GetTestByLessonId([FromRoute] int id)
    {
        return await _testService.GetTestsByLessonAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<CreateTestResponseDto>> CreateTest([FromBody] CreateTestRequestDto request)
    {
        return Ok(await _testService.CreateTestAsync(request));
    }
}