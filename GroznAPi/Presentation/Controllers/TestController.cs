using Contracts.Aws;
using Contracts.Test;
using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using Service.Abstactions;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class TestController : BaseController
{
    private readonly ILogger<TestController> _logger;
    private readonly ITestService _testService;
    private readonly AwsService _awsService;

    public TestController(ILogger<TestController> logger, ITestService testService, AwsService awsService)
    {
        _logger = logger;
        _testService = testService;
        _awsService = awsService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<TestDto>>> GetAllTests()
    {
        return await _testService.GetAllTestsAsync();
    }

    [HttpGet("by-lesson/{id}")]
    public async Task<ActionResult> GetTestByLessonId([FromRoute] int id)
    {
        return Ok(await _testService.GetTestsByLessonAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<CreateTestResponseDto>> CreateTest([FromBody] CreateTestRequestDto request)
    {
        return Ok(await _testService.CreateTestAsync(request));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTest(UpdateTestRequestDto request)
    {
        return Ok(await _testService.UpdateTestAsync(request));
    }

    [HttpPost("upload-resource/{testId}/{questionId}")]
    public async Task<ActionResult> UploadResourceAsync(IFormFile formFile, [FromRoute] int questionId, [FromRoute] int testId)
    {
        if (formFile.Length == 0) throw new ResourceNotUploadedException("Resource has not been uploaded to server");

        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);

        var uploaded = await _awsService.UploadFile(new AwsFileUploadDto
        {
            FileName = formFile.FileName,
            Stream = memoryStream,
            Type = formFile.ContentType
        });

        if (!uploaded.IsUploaded) throw new ResourceNotUploadedException("Resource has not been uploaded to server");

        return Ok(await _testService.SetQuestionResourceAsync(testId, questionId, new Resource
        {
            Name = formFile.FileName,
            Url = uploaded.File!.Url,
            Type = uploaded.File!.Type
        }));
    }
}