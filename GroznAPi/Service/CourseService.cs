using Contracts.Course;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task CreateCourseAsync(CreateCourseRequestDto request)
    {
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Teacher = 
        }
    }
}