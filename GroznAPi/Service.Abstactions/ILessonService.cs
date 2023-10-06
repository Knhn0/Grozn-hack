using Contracts.Analitic;
using Contracts.Lesson;
using Domain.Entities;

namespace Service.Abstactions;

public interface ILessonService
{
    Task<ThemeResponseDto> GetAllTestsByThemeId(int themeId, int studentId);
    Task<GetAllLessonsResponseDto> GetAllAsync();

    Task<GetLessonResponseDto> GetLesson(GetLessonRequestDto req);

    Task<UpdateLessonResponseDto> UpdateLesson(UpdateLessonRequestDto req);

    Task<CreateLessonResponseDto> CreateLesson(CreateLessonRequestDto req);

    Task<DeleteLessonResponseDto> DeleteLesson(DeleteLessonRequestDto req);

    Task<GetLessonThemeResponseDto> GetTheme(GetLessonThemeRequestDto req);

}