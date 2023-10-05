using Contracts.Course;
using Domain.Entities;

namespace Service.Abstactions;

public interface ICourseService
{
    Task<CourseCreatedResponseDto> CreateCourseAsync(CreateCourseRequestDto request, int userId);
    Task<CourseJoinedResponseDto> JoinCourseAsync(JoinCourseRequestDto request, int userId);
    Task<GetThemesResponseDto> GetThemesAsync(GetThemesRequestDto request);
    Task<List<Course>> GetJoinedCourses(int userId);
    Task<List<Course>> GetAllCoursesAsync();
    Task<Course> GetByIdAsync(int courseId);
    Task<CourseRemovedResponseDto> RemoveCourseForcedAsync(RemoveCourseRequestDto request);
    Task<CourseRemovedResponseDto> RemoveCourseAsync(RemoveCourseRequestDto request, int userId);
}