using Newtonsoft.Json;

namespace Contracts.Gpt;

public class GptQuestionRequestDto
{
    [JsonRequired] public string title { get; set; }
    [JsonRequired] public int diff { get; set; }
}