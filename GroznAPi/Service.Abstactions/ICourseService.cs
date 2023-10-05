using Contracts.Course;

namespace Service.Abstactions;

public interface ICourseService
{
    Task<CourseCreatedResponseDto> CreateCourseAsync(CreateCourseRequestDto request);
    Task<CourseJoinedResponseDto> JoinCourseAsync(JoinCourseRequestDto request, int userId);
    Task<GetThemesResponseDto> GetThemesAsync(GetThemesRequestDto request);
}