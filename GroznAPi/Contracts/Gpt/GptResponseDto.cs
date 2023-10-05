namespace Contracts.Gpt;

public class GptResponseAnswerDto
{
    public string Text { get; set; }
    public bool IsRight { get; set; }
}

public class GptResponseQuestionDto
{
    public string Title { get; set; }
    public List<GptResponseAnswerDto> Answers { get; set; }
}

public class GptResponseMessageDto
{
    public string Text { get; set; }
    public DateTime Datetime { get; set; }
    public string RequestText { get; set; } 
}