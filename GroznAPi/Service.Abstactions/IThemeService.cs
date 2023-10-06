using Contracts.Theme;
using Domain.Entities;

namespace Service.Abstactions;

public interface IThemeService
{
    Task<CreateThemeResponseDto> CreateThemeAsync(CreateThemeRequestDto req);
    Task<GetThemeResponseDto> GetThemeAsync(int id);
    Task<UpdateThemeResponseDto> UpdateThemeAsync(UpdateThemeRequestDto req);
    Task DeleteThemeAsync(DeleteThemeRequestDto req);
    Task<GetLessonsResponseDto> GetLessonsAsync();
}