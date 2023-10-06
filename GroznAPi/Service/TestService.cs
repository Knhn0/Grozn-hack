using System.Collections.ObjectModel;
using Contracts.Aws;
using Contracts.Course;
using Contracts.Test;
using Domain.Entities;
using Exceptions.Implementation;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly IQuestionRepository _questionRepository;

    public TestService(ITestRepository testRepository, IQuestionRepository questionRepository)
    {
        _testRepository = testRepository;
        _questionRepository = questionRepository;
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

    public async Task<UpdateTestResponseDto> UpdateTestAsync(UpdateTestRequestDto request)
    {
        var testDb = await _testRepository.GetByIdAsync(request.Id);

        testDb.Title = request.Title;
        testDb.Description = request.Description;
        testDb.LessonId = request.LessonId;
        testDb.Questions = request.Questions.Select(q => new Question
        {
            Title = q.Title,
            Answers = q.Answers.Select(a => new Answer
            {
                Title = a.Title,
                IsRight = a.IsRight
            }).ToList()
        }).ToList();

        var res = await _testRepository.UpdateAsync(testDb);
        
        return new UpdateTestResponseDto
        {
            Test = new TestDto
            {
                Id = testDb.Id,
                Title = testDb.Title,
                Description = testDb.Description,
                Questions = testDb.Questions.Select(q => new QuestionDto
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
                LessonId = testDb.LessonId
            }
        };
    }

    public async Task<bool> RemoveTestAsync(int id)
    {
        var res = await _testRepository.DeleteAsync(new Test { Id = id });
        return res;
    }

    public async Task<AwsFileDto> SetQuestionResourceAsync(int testId, int questionId, Resource resource)
    {
        var questions = await _testRepository.GetQuestionsAsync(testId);
        var candidate = questions.FirstOrDefault(q => q.Id == questionId);
        
        if (candidate is null) throw new QuestionNotFoundException("Question not found");
        candidate.Resource = resource;
        await _questionRepository.UpdateAsync(candidate);
        
        return new AwsFileDto
        {
            Type = candidate.Resource.Type,
            Url = candidate.Resource.Url
        };
    }
}