using Contracts.Course;

namespace Service.Abstactions;

public interface ICourseService
{
    Task<CourseCreatedResponseDto> CreateCourseAsync(CreateCourseRequestDto request);
    Task<CourseJoinedResponseDto> JoinCourse(JoinCourseRequestDto request, int userId);
}