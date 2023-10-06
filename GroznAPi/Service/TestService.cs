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

    public async Task<CreateTestResponseDto> CreateTestAsync(CreateTestRequestDto request)
    {
        var test = await _testRepository.CreateAsync(new Test
        {
            Title = request.Title,
            Description = request.Description,
            Questions = request.Questions.Select(q => new Question
            {
                Title = q.Title,
                Answers = q.Answers.Select(a => new Answer
                {
                    Title = a.Title,
                    IsRight = a.IsRight
                }).ToList()
            }).ToList()
        });

        return new CreateTestResponseDto
        {
            Test = new TestDto
            {
                Id = test.Id,
                Title = test.Title,
                Description = test.Description,
                Questions = test.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        IsRight = a.IsRight
                    }).ToList()
                }).ToList(),
                LessonId = test.LessonId
            }
        };
    }

    public async Task<List<TestDto>> GetAllTestsAsync()
    {
        var tests = await _testRepository.GetAllAsync();

        return new List<TestDto>(tests.Select(t => new TestDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Questions = t.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                Answers = q.Answers.Select(a => new AnswerDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    IsRight = a.IsRight
                }).ToList()
            }).ToList(),
            LessonId = t.LessonId
        }));
    }

    public async Task<List<TestDto>> GetTestsByLessonAsync(int lessonId)
    {
        var tests = await _testRepository.GetAllTestsByLessonIdAsync(lessonId);
        
        return new List<TestDto>(tests.Select(t => new TestDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Questions = t.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                Answers = q.Answers.Select(a => new AnswerDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    IsRight = a.IsRight
                }).ToList()
            }).ToList(),
            LessonId = t.LessonId
        }));
    }
    
}