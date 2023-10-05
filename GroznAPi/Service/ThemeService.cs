using Contracts.Theme;
using Domain.Entities;
using Presistence;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class ThemeService: IThemeService
{
    private readonly IThemeRepository _themeRepository;

    public ThemeService(IThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }

    public async Task<CreateThemeResponseDto> CreateTheme(CreateThemeRequestDto req)
    {
        var theme = new Theme
        {
            Id = req.ThemeId,
            Title = req.Tiile,
            Description = req.Description,
            Lessons = req.Lessons
        };
        await _themeRepository.CreateAsync(theme);
        return new CreateThemeResponseDto
        {
            Title = theme.Title,
            Description = theme.Description,
            ThemeId = theme.Id,
            Lessons = theme.Lessons
        };
    }

    public async Task<GetThemeResponseDto> GetTheme(int id)
    {
        if (id == 0) throw new Exception("Invalid id");
        var res = await _themeRepository.GetByIdAsync(id);
        if (res == null) throw new Exception("Theme not found");
        return new GetThemeResponseDto
        {
            Title = res.Title,
            Description = res.Description,
            Lessons = res.Lessons
            
        };
    }

    public async Task<UpdateThemeResponseDto> UpdateTheme(UpdateThemeRequestDto t)
    {
        var theme = await _themeRepository.GetByIdAsync(t.ThemeId);
        if (theme == null) throw new Exception("Theme not found");
        if (!String.IsNullOrEmpty(t.Title)) theme.Title = t.Title;
        if (!String.IsNullOrEmpty(t.Description)) theme.Description = t.Description;
        await _themeRepository.UpdateAsync(theme);
        return new UpdateThemeResponseDto
        {
            Tiile = theme.Title,
            Description = theme.Description,
            Lessons = theme.Lessons
        };
    }

    public async Task DeleteTheme(DeleteThemeRequestDto t)
    {
        var theme = await _themeRepository.GetByIdAsync(t.ThemeId);
        if (theme == null) throw new Exception("Theme not found");
        await _themeRepository.DeleteAsync(theme);
    }
    

    public async Task<GetLessonsResponseDto> GetLessons()
    {
        var list = await _themeRepository.GetLessonsAsync();
        return new GetLessonsResponseDto
        {
            Lessons = list
        };
    }
}