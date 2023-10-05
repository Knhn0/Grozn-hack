using Contracts.Course;
using Domain.Entities;
using Exceptions;
using Exceptions.Implementation;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;

    public CourseService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
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

    public async Task<GetThemesResponseDto> GetThemesAsync(GetThemesRequestDto request)
    {
        var themes = await _courseRepository.GetThemesById(request.CourseId);
        var dtos = themes.Select(t => new GetThemesResponseDto.ThemeDto
        {
            Title = t.Title, Description = t.Description
        }).ToList();
        return new GetThemesResponseDto
        {
            Themes = dtos
        };
    }

    public async Task<CourseJoinedResponseDto> JoinCourseAsync(JoinCourseRequestDto request, int userId)
    {
        var student = await _studentRepository.GetByUserIdAsync(userId);
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