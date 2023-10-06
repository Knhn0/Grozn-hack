namespace Contracts.Test;

public class UpdateTestRequestDto
{
    public class Question
    {
        public string Title { get; set; }
        public List<UpdateTestRequestDto.Answer> Answers { get; set; }
    }

    public class Answer
    {
        public string Title { get; set; }
        public bool IsRight { get; set; }
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<UpdateTestRequestDto.Question> Questions { get; set; }
}

/*
interface UpdateTestDto {
    title: string,
    description: string,
    questions: Array<{
            title: string,
            testId: number,
            answers: Array<{
                name: string,
                questionId: number,
                isRight: boolean
            }>
        }>
*/