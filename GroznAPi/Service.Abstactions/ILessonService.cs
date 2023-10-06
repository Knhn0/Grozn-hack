using Contracts.Analitic;

namespace Service.Abstactions;

public interface ILessonService
{
    Task<ThemeResponseDto> GetAllTestsByThemeId(int themeId, int studentId);
}