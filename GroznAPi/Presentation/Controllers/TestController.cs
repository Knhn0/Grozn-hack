using Contracts.Test;
using Domain.Entities;
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
    public async Task<ActionResult<List<Test>>> GetAllTests()
    {
        return await _testService.GetAllTests();
    }

    [HttpPost]
    public async Task<ActionResult<TestCreatedResponse>> CreateTest(CreateTestRequestDto request)
    {
        return Ok(await _testService.CreateTestAsync(request));
    }
}