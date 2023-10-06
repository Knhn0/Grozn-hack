using Contracts.Lesson;
using Contracts.Test;
using Contracts.Theme;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class ThemeService : IThemeService
{
    private readonly IThemeRepository _themeRepository;

    public ThemeService(IThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }

    public async Task<CreateThemeResponseDto> CreateThemeAsync(CreateThemeRequestDto req)
    {
        var theme = new Theme
        {
            Title = req.Title,
            Description = req.Description,
            CourseId = req.CourseId,
            Lessons = req.Lessons.Select(x => new Lesson
            {
                Title = x.Title,
                ThemeId = x.ThemeId,
                ArticleBody = x.ArticleBody,
                Tests = x.Tests.Select(x => new Test
                {
                    Title = x.Title,
                    LessonId = x.LessonId,
                    Questions = x.Questions.Select(x => new Question
                    {
                        Title = x.Title,
                        TestId = x.TestId,
                        Answers = x.Answers.Select(x => new Answer
                        {
                            Title = x.Title,
                            IsRight = x.IsRight,
                            QuestionId = x.QuestionId
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList()
        };
        await _themeRepository.CreateAsync(theme);
        return new CreateThemeResponseDto
        {
            Theme = new ThemeDto()
            {
                Id = theme.Id,
                CourseId = theme.CourseId,
                Title = theme.Title,
                Description = theme.Description,
                Lessons = theme.Lessons.Select(x => new LessonDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    ArticleBody = x.ArticleBody,
                    ThemeId = x.ThemeId,
                    Tests = x.Tests.Select(x => new TestDto
                    {
                        Id = x.Id,
                        Title = x.Title,
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
                                IsRight = x.IsRight,
                                QuestionId = x.QuestionId
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
    }

    public async Task<GetThemeResponseDto> GetThemeAsync(int id)
    {
        if (id == 0) throw new Exception("Invalid id");
        var res = await _themeRepository.GetByIdAsync(id);
        if (res == null) throw new Exception("Theme not found");
        return new GetThemeResponseDto
        {
            Theme = new ThemeDto
            {
                Id = res.Id,
                CourseId = res.CourseId,
                Title = res.Title,
                Description = res.Description,
                Lessons = res.Lessons.Select(x => new LessonDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    ArticleBody = x.ArticleBody,
                    ThemeId = x.ThemeId,
                    Tests = x.Tests.Select(x => new TestDto
                    {
                        Id = x.Id,
                        Title = x.Title,
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
                                IsRight = x.IsRight,
                                QuestionId = x.QuestionId
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
    }

    public async Task<UpdateThemeResponseDto> UpdateThemeAsync(UpdateThemeRequestDto t)
    {
        var theme = await _themeRepository.GetByIdAsync(t.Theme.Id);
        if (theme == null) throw new Exception("Theme not found");
        if (!String.IsNullOrEmpty(t.Theme.Title)) theme.Title = t.Theme.Title;
        if (!String.IsNullOrEmpty(t.Theme.Description)) theme.Description = t.Theme.Description;
        var res = await _themeRepository.UpdateAsync(theme);
        return new UpdateThemeResponseDto
        {
            Theme = new ThemeDto
            {
                Id = res.Id,
                CourseId = res.CourseId,
                Title = res.Title,
                Description = res.Description,
                Lessons = res.Lessons.Select(x => new LessonDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    ArticleBody = x.ArticleBody,
                    ThemeId = x.ThemeId,
                    Tests = x.Tests.Select(x => new TestDto
                    {
                        Id = x.Id,
                        Title = x.Title,
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
                                IsRight = x.IsRight,
                                QuestionId = x.QuestionId
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
    }

    public async Task DeleteThemeAsync(int id)
    {
        var theme = await _themeRepository.GetByIdAsync(id);
        if (theme == null) throw new Exception("Theme not found");
        await _themeRepository.DeleteAsync(id);
    }

   
    public async Task<List<ThemeDto>> GetThemesByCourseId(int courseId)
    {
        var db = await _themeRepository.GetByCourseId(courseId);
        var res = db.Select(t => new ThemeDto
        {
            Title = t.Title,
            Description = t.Description,
            Id = t.Id
        });
        return res.ToList();
    }
}