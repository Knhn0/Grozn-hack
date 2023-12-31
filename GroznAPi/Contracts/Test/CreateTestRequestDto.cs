﻿namespace Contracts.Test;

public class CreateTestRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int LessonId { get; set; }
    public List<QuestionDto> Questions { get; set; }
}