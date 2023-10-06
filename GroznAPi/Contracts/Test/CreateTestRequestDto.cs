﻿namespace Contracts.Test;

public class CreateTestRequestDto
{
    public class Question
    {
        public string Title { get; set; }
        public List<CreateTestRequestDto.Answer> Answers { get; set; }
    }

    public class Answer
    {
        public string Title { get; set; }
        public bool IsRight { get; set; }
    }
    
    public int LessonId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CreateTestRequestDto.Question> Questions { get; set; }
}

/*
interface CreateTestDto {
    title: string,
    description: string,
    questions: Array<{
            title: string,
            answers: Array<{
                name: string,
                isRight: boolean
            }>
        }>
}
*/