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
    private readonly ITeacherRepository _teacherRepository;

    public CourseService(IStudentRepository studentRepository, ICourseRepository courseRepository, ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetAllAsync();
    }

    public async Task<Course> GetByIdAsync(int courseId)
    {
        return await _courseRepository.GetByIdAsync(courseId);
    }

    public async Task<List<Course>> GetJoinedCourses(int userId)
    {
        var student = await _studentRepository.GetByUserIdAsync(userId);
        return await _studentRepository.GetJoinedCoursesAsync(student.Id);
    }

    public async Task<List<Course>> GetCreatedCourses(int userId)
    {
        var teacher = await _teacherRepository.GetByUserIdAsync(userId);
        return await _teacherRepository.GetCreatedCoursesAsync(teacher.Id);
    }
    
    public async Task<CourseCreatedResponseDto> CreateCourseAsync(CreateCourseRequestDto request, int userId)
    {
        var teacher = await _teacherRepository.GetByUserIdAsync(userId);
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Teacher = teacher
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

    public async Task<CourseRemovedResponseDto> RemoveCourseForcedAsync(RemoveCourseRequestDto request)
    {
        var result = await _courseRepository.DeleteAsync(await _courseRepository.GetByIdAsync(request.Id));
        if (!result) throw new CourseNotFoundException("Course not found");
        return new CourseRemovedResponseDto
        {
            Id = request.Id
        };
    }

    public async Task<CourseRemovedResponseDto> RemoveCourseAsync(RemoveCourseRequestDto request, int userId)
    {
        var result = await _teacherRepository.GetCreatedCoursesAsync((await _teacherRepository.GetByUserIdAsync(userId)).Id);
        var candidate = result.FirstOrDefault(c => c.Id == request.Id);
        if (candidate == null)
            throw new CourseNotFoundException("Course not found");

        await _courseRepository.DeleteAsync(candidate);
        return new CourseRemovedResponseDto
        {
            Id = candidate.Id,
            Title = candidate.Title
        };
    }
}