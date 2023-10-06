using Contracts.Lesson;

namespace Service.Abstactions;

public interface ILessonService
{
    Task<List<GetLessonsResponseDto>> GetAllAsync();

    Task<GetLessonResponseDto> GetLesson(int id);

    Task<UpdateLessonResponseDto> UpdateLesson(UpdateLessonRequestDto req);

    Task<CreateLessonResponseDto> CreateLesson(CreateLessonRequestDto req);

    Task DeleteLesson(int id);
}