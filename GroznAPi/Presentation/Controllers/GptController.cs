using System.ComponentModel.DataAnnotations;
using Contracts.Gpt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class GptController
{
    private readonly ILogger<LessonController> _logger;
    private readonly GptService _gptService;
    
    public GptController(ILogger<LessonController> logger, GptService gptService)
    {
        _logger = logger;
        _gptService = gptService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("gen/message/{reqText}")]
    public async Task<ActionResult<GptResponseMessageDto>> GenMessage([Required] string reqText)
    {
        return await _gptService.GenMessage(reqText);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("gen/question/")]
    public async Task<ActionResult<GptResponseQuestionDto>> GenQuestion([FromBody] GptQuestionRequestDto gptQuestionRequestDto)
    {
        return await _gptService.GenQuestion(gptQuestionRequestDto);
    }
}