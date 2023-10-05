using Contracts.Course;
using Domain.Entities;
using Exceptions;
using Exceptions.Implementation;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class CourseService : ICourseRepository
{
    private readonly ICourseRepository _courseRepository;
    private readonly object _userInfoService; // todo: change after user info implementation

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseCreatedResponseDto> CreateCourseAsync(CreateCourseRequestDto request)
    {
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Teacher = new() // todo: change after user info implementation
        };
        
        var res = await _courseRepository.CreateAsync(course);
        
        return new CourseCreatedResponseDto
        {
            Description = res.Description,
            Title = res.Title,
            CourseId = res.Id
        };
    }

    public async Task<CourseJoinedResponseDto> JoinCourse(JoinCourseRequestDto request, int userId)
    {
        var student = _userInfoService.GetStudent(userId);
        if (student is null) throw new StudentNotFoundException("Student with such not found");
        var res = await _courseRepository.AddStudent(request.CourseId, student);
        if (res is null) throw new CourseNotFoundException("Course with such id not found");
        return new CourseJoinedResponseDto
        {
            CourseId = res.Id,
            UserId = student.Id
        };
    }
}