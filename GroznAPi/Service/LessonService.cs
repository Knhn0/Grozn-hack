using Contracts.Analitic;
using Contracts.Lesson;
using Contracts.Test;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ITestRepository _testRepository;
    private readonly ITestPercentRepository _testPercentRepository;
    private readonly IThemeRepository _themeRepository;

    public LessonService(ILessonRepository lessonRepository, ITestRepository testRepository, ITestPercentRepository testPercentRepository, IThemeRepository themeRepository)
    {
        _lessonRepository = lessonRepository;
        _testRepository = testRepository;
        _testPercentRepository = testPercentRepository;
        _themeRepository = themeRepository;
    }

    public async Task<ThemeResponseDto> GetAllTestsByThemeId(int themeId, int studentId)
    {
        var lessons = await _lessonRepository.GetLessonsByThemeIdAsync(themeId);
        var tests = new List<TestResponseDto>();

        for (int i = 0; i < lessons.Count; i++)
        {
            var thisTests = await _testRepository.GetAllTestsByLessonIdAsync(lessons[i].Id);

            for (int l = 0; l < thisTests.Count(); l++)
            {
                var res = await _testPercentRepository.GetTestPercentByTestIdAndStudentId(thisTests[l].Id, studentId);
                for (int m = 0; m < res.Count; m++)
                {
                    var ftest = new TestResponseDto();
                    ftest.TestBalls = Math.Pow(res[m].Percent * 100, 0);
                    ftest.LeftBalls = 100 - ftest.TestBalls - ftest.TestBalls;

                    tests.Add(ftest);
                }
            }
        }

        var response = new ThemeResponseDto();
        response.Tests = tests;

        return response;
    }

    public async Task<List<GetLessonsResponseDto>> GetAllAsync()
    {
        var list = await _lessonRepository.GetAllAsync();
        return list.Select(x => new GetLessonsResponseDto
        {
            Lesson = new LessonDto
            {
                Id = x.Id,
                Title = x.Title,
                ArticleBody = x.ArticleBody,
                ThemeId = x.ThemeId,
                Tests = x.Tests.Select(x => new TestDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    LessonId = x.LessonId,
                    Questions = x.Questions.Select(x => new QuestionDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        TestId = x.TestId,
                        Answers = x.Answers.Select(x => new AnswerDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            QuestionId = x.QuestionId,
                            IsRight = x.IsRight
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        }).ToList();
    }

    public async Task<GetLessonResponseDto> GetLesson(int id)
    {
        var res = await _lessonRepository.GetByIdAsync(id);
        if (res == null) throw new Exception("Lesson not found");
        return new GetLessonResponseDto
        {
            Lesson = new LessonDto
            {
                Id = res.Id,
                Title = res.Title,
                ArticleBody = res.ArticleBody,
                ThemeId = res.ThemeId,
                Tests = res.Tests.Select(x => new TestDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    LessonId = x.LessonId,
                    Questions = x.Questions.Select(x => new QuestionDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        TestId = x.TestId,
                        Answers = x.Answers.Select(x => new AnswerDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            QuestionId = x.QuestionId,
                            IsRight = x.IsRight
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
    }

    public async Task<UpdateLessonResponseDto> UpdateLesson(UpdateLessonRequestDto req)
    {
        if (req.Lesson.Id == 0) throw new Exception("Id is not valid");
        var res = await _lessonRepository.GetByIdAsync(req.Lesson.Id);
        if (!String.IsNullOrEmpty(res.Title)) res.Title = req.Lesson.Title;
        if (!String.IsNullOrEmpty(res.ArticleBody)) res.ArticleBody = req.Lesson.ArticleBody;
        if (req.Lesson.ThemeId != null) res.ThemeId = req.Lesson.ThemeId;
        await _lessonRepository.UpdateAsync(res);
        return new UpdateLessonResponseDto
        {
            Lesson = new LessonDto()
            {
                Id = res.Id,
                Title = res.Title,
                ArticleBody = res.ArticleBody,
                ThemeId = res.ThemeId,
                Tests = res.Tests.Select(x => new TestDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    LessonId = x.LessonId,
                    Questions = x.Questions.Select(x => new QuestionDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        TestId = x.TestId,
                        Answers = x.Answers.Select(x => new AnswerDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            QuestionId = x.QuestionId,
                            IsRight = x.IsRight
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
    }

    public async Task<CreateLessonResponseDto> CreateLesson(CreateLessonRequestDto req)
    {
        var lesson = new Lesson
        {
            Id = req.Id,
            Title = req.Title,
            ArticleBody = req.ArticleBody,
            ThemeId = req.ThemeId,
            Tests = req.Tests.Select(x => new Test
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                LessonId = x.LessonId,
                Questions = x.Questions.Select(x => new Question
                {
                    Id = x.Id,
                    Title = x.Title,
                    TestId = x.TestId,
                    Answers = x.Answers.Select(x => new Answer
                    {
                        Id = x.Id,
                        Title = x.Title,
                        QuestionId = x.QuestionId,
                        IsRight = x.IsRight
                    }).ToList()
                }).ToList()
            }).ToList()
        };

        await _lessonRepository.CreateAsync(lesson);
        return new CreateLessonResponseDto
        {
            Lesson = new LessonDto()
            {
                Id = lesson.Id,
                Title = lesson.Title,
                ArticleBody = lesson.ArticleBody,
                ThemeId = lesson.ThemeId,
                Tests = lesson.Tests.Select(x => new TestDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    LessonId = x.LessonId,
                    Questions = x.Questions.Select(x => new QuestionDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        TestId = x.TestId,
                        Answers = x.Answers.Select(x => new AnswerDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            QuestionId = x.QuestionId,
                            IsRight = x.IsRight
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
    }

    public async Task DeleteLesson(int id)
    {
        await _lessonRepository.DeleteAsync(id);
    }
    
    public async Task<GetLessonsPercentResponseDto> GetLessonsPercentByThemeId(int themeId, int studentId)
    {
        var lessons = await _lessonRepository.GetLessonsByThemeIdAsync(themeId);
        var listOfLessonPercentDtos = new List<LessonPercentDto>();
        var res = new GetLessonsPercentResponseDto();
        
        for (int i = 0; i < lessons.Count(); i++)
        {
            var testPercents = await _testPercentRepository.GetTestPercentByTestIdAndStudentId(lessons[i].Id, studentId);
            double percent = 0;
            foreach (var testPercent in testPercents)
            {
                percent += testPercent.Percent / testPercents.Count();
            }

            var lessonPercentDto = new LessonPercentDto();
            lessonPercentDto.Percent = percent;
            lessonPercentDto.Title = lessons[i].Title;
            
            listOfLessonPercentDtos.Add(lessonPercentDto);
        }
        res.lessons = listOfLessonPercentDtos;
        return res;
    }
}