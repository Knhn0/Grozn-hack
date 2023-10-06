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
   
    public async Task<GetAllLessonsResponseDto> GetAllAsync()
    {
        var list = await _lessonRepository.GetAllAsync();
        return new GetAllLessonsResponseDto
        {
            Lessons = list
        };
    }

    public async Task<GetLessonResponseDto> GetLesson(GetLessonRequestDto req)
    {
        if (req.LessonId == 0) throw new Exception("Id is not valid");
        var res = await _lessonRepository.GetByIdAsync(req.LessonId);
        if (res == null) throw new Exception("Lesson not found");
        return new GetLessonResponseDto
        {
            Title = res.Title,
            ArticleBody = res.ArticleBody
        };
    }

    public async Task<UpdateLessonResponseDto> UpdateLesson(UpdateLessonRequestDto req)
    {
        if (req.LessonId == 0) throw new Exception("Id is not valid");
        var res = await _lessonRepository.GetByIdAsync(req.LessonId);
        if (!String.IsNullOrEmpty(res.Title)) res.Title = req.Title;
        if (!String.IsNullOrEmpty(res.ArticleBody)) res.ArticleBody = req.ArticleBody;
        if (req.ThemeId != null) res.ThemeId = req.ThemeId;
        await _lessonRepository.UpdateAsync(res);
        return new UpdateLessonResponseDto
        {
            ThemeId = res.ThemeId,
            Title = res.Title,
            ArticleBody = res.ArticleBody
        };
    }

    public async Task<CreateLessonResponseDto> CreateLesson(CreateLessonRequestDto req)
    {
        var lesson = new Lesson
        {
            Id = req.LessonId,
            Title = req.Title,
            ArticleBody = req.ArticleBody,
            ThemeId = req.ThemeId
        };

        await _lessonRepository.CreateAsync(lesson);
        return new CreateLessonResponseDto
        {
            LessonId = lesson.Id,
            Title = lesson.Title,
            ArticleBody = lesson.ArticleBody
        };
    }

    public async Task<DeleteLessonResponseDto> DeleteLesson(DeleteLessonRequestDto req)
    {

        var lesson = new Lesson
        {
            Id = req.LessonId,
            ArticleBody = req.ArticleBody,
            Title = req.Title
        };

        await _lessonRepository.DeleteAsync(lesson);
        return new DeleteLessonResponseDto
        {
            Title = lesson.Title,
            ArticleBody = lesson.ArticleBody,
            LessonId = lesson.Id
        };
    }

    public async Task<GetLessonThemeResponseDto> GetTheme(GetLessonThemeRequestDto req)
    {
        if (req.LessonId == 0) throw new Exception("id is not valid");
        var res = await _lessonRepository.GetByIdAsync(req.LessonId);
        if (res == null) throw new Exception("Lesson not found");
        return new GetLessonThemeResponseDto
        {
            Theme = await _themeRepository.GetByIdAsync(req.)
        };
    }
}