using Contracts.Theme;

namespace Service.Abstactions;

public interface IThemeService
{
    Task<CreateThemeResponseDto> CreateThemeAsync(CreateThemeRequestDto req);
    Task<GetThemeResponseDto> GetThemeAsync(int id);
    Task<UpdateThemeResponseDto> UpdateThemeAsync(UpdateThemeRequestDto req);
    Task DeleteThemeAsync(int id);
    Task<List<ThemeDto>> GetThemesByCourseId(int courseId);
}