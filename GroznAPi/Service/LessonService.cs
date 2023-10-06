using Contracts.Lesson;
using Contracts.Test;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;

    public LessonService(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
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
            ArticleBody = res.ArticleBody,
            theme = res.Theme
        };
    }

    public async Task<UpdateLessonResponseDto> UpdateLesson(UpdateLessonRequestDto req)
    {
        if (req.LessonId == 0) throw new Exception("Id is not valid");
        var res = await _lessonRepository.GetByIdAsync(req.LessonId);
        if (!String.IsNullOrEmpty(res.Title)) res.Title = req.Title;
        if (!String.IsNullOrEmpty(res.ArticleBody)) res.ArticleBody = req.ArticleBody;
        if (req.theme != null) res.Theme = req.theme;
        await _lessonRepository.UpdateAsync(res);
        return new UpdateLessonResponseDto
        {
            theme = res.Theme,
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
            Theme = req.theme
        };

        await _lessonRepository.CreateAsync(lesson);
        return new CreateLessonResponseDto
        {
            LessonId = lesson.Id,
            theme = lesson.Theme,
            Title = lesson.Title,
            ArticleBody = lesson.ArticleBody
        };
    }

    public async Task<DeleteLessonResponseDto> DeleteLesson(DeleteLessonRequestDto req)
    {

        var lesson = new Lesson
        {
            Id = req.LessonId,
            Theme = req.theme,
            ArticleBody = req.ArticleBody,
            Title = req.Title
        };

        await _lessonRepository.DeleteAsync(lesson);
        return new DeleteLessonResponseDto
        {
            theme = lesson.Theme,
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
            Theme = res.Theme
        };
    }
}