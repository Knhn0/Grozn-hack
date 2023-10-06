using Contracts.Lesson;

namespace Service.Abstactions;

public interface ILessonService
{
    Task<GetAllLessonsResponseDto> GetAllAsync();

    Task<GetLessonResponseDto> GetLesson(GetLessonRequestDto req);

    Task<UpdateLessonResponseDto> UpdateLesson(UpdateLessonRequestDto req);

    Task<CreateLessonResponseDto> CreateLesson(CreateLessonRequestDto req);

    Task<DeleteLessonResponseDto> DeleteLesson(DeleteLessonRequestDto req);

    Task<GetLessonThemeResponseDto> GetTheme(GetLessonThemeRequestDto req);
}