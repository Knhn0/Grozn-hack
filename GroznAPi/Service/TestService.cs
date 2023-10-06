using System.Collections.ObjectModel;
using Contracts.Test;
using Domain.Entities;
using Exceptions.Implementation;
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

        /*
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
         */
        
        
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

    public async Task<TestDto> EditTestAsync(TestDto testDto)
    {
        var testDb = await _testRepository.GetByIdAsync(testDto.Id);

        testDb.Title = testDto.Title;
        testDb.Description = testDto.Description;
        testDb.LessonId = testDto.LessonId;
        testDb.Questions = testDto.Questions.Select(q => new Question
        {
            Title = q.Title,
            Answers = q.Answers.Select(a => new Answer
            {
                Title = a.Title,
                IsRight = a.IsRight
            }).ToList()
        }).ToList();

        var res = await _testRepository.UpdateAsync(testDb);

        return testDto;
    }

    public async Task<TestDto> RemoveTestAsync(TestDto testDto)
    {
        var res = await _testRepository.DeleteAsync(new Test
        {
            Title = testDto.Title,
            Description = testDto.Description,
            Questions = testDto.Questions.Select(q => new Question
            {
                Title = q.Title,
                Answers = q.Answers.Select(a => new Answer
                {
                    Title = a.Title,
                    IsRight = a.IsRight
                }).ToList()
            }).ToList()
        });

        if (!res) throw new TestNotFoundException("Invalid test");
        return testDto;
    }
}