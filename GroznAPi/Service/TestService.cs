using System.Collections.ObjectModel;
using Contracts.Test;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly ILessonRepository _lessonRepository;

    public TestService(ITestRepository testRepository, ILessonRepository lessonRepository)
    {
        _testRepository = testRepository;
        _lessonRepository = lessonRepository;
    }
    
    public async Task<TestCreatedResponse> CreateTestAsync(CreateTestRequestDto request)
    {
        var test = await _testRepository.CreateAsync(new Test
        {
            Title = request.Title,
            Description = request.Description
        });

        var questions = new List<Question>();
        foreach (var q in request.Questions)
        {
            var question = new Question
            {
                Title = q.Title,
                Test = test,
                Answers = new Collection<Answer>()
            };

            foreach (var a in q.Answers)
            {
                var answer = new Answer
                {
                    Text = a.Title,
                    IsRight = a.IsRight,
                    Question = question,
                    QuestionId = question.Id
                };
                question.Answers.Add(answer);
            }
            questions.Add(question);
        }
        
        var dbQuestions = await _testRepository.CreateQuestionsAsync(test.Id, questions.ToArray());

        var response = request as TestCreatedResponse;
        response.Id = test.Id;

        var lessonDb = await _lessonRepository.GetByIdAsync(request.LessonId);

        await _testRepository.UpdateAsync(new Test
        {
            Lesson = lessonDb,
            Title = test.Title,
            Description = test.Description
        });

        return response;
    }

    public async Task<List<Test>> GetAllTestsAsync()
    {
        return await _testRepository.GetAllAsync();
    }

    public async Task<List<Test>> GetTestsByLessonAsync(int lessonId)
    {
        return await _testRepository.GetAllTestsByLessonIdAsync(lessonId);
    }

    public async Task<Test> GetTestByIdAsync(int id)
    {
        return await _testRepository.GetByIdAsync(id);
    }
}