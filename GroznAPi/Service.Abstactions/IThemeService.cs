using Contracts.Theme;
using Domain.Entities;

namespace Service.Abstactions;

public interface IThemeService
{
    Task<CreateThemeResponseDto> CreateTheme(CreateThemeRequestDto req);
    Task<GetThemeResponseDto> GetTheme(int id);
    Task<UpdateThemeResponseDto> UpdateTheme(UpdateThemeRequestDto req);
    Task DeleteTheme(DeleteThemeRequestDto req);
    Task<GetLessonsResponseDto> GetLessons();
}