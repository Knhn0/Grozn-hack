using Contracts.Course;
using Domain.Entities;

namespace Service.Abstactions;

public interface ICourseService
{
    Task<CreateCourseResponseDto> CreateCourseAsync(CreateCourseRequestDto request, int userId);
    Task<CourseJoinedResponseDto> JoinCourseAsync(JoinCourseRequestDto request, int userId);
    Task<List<Course>> GetJoinedCourses(int userId);
    Task<List<Course>> GetAllCoursesAsync();
    Task<Course> GetByIdAsync(int courseId);
    Task RemoveCourseForcedAsync(int id);
    Task RemoveCourseAsync(int id, int userId);
    Task<UpdateCourseResponseDto> UpdateCourseAsync(UpdateCourseRequestDto request, int userId);
    Task<UpdateCourseResponseDto> UpdateCourseForcedAsync(UpdateCourseRequestDto request);
}