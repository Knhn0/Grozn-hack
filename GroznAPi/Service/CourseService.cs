using Contracts.Course;
using Contracts.Lesson;
using Contracts.Test;
using Contracts.Theme;
using Domain.Entities;
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


    public async Task<CreateCourseResponseDto> CreateCourseAsync(CreateCourseRequestDto request, int userId)
    {
        var teacher = await _teacherRepository.GetByUserIdAsync(userId);
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Teacher = teacher,
            Themes = request.Themes.Select(t => new Theme
            {
                Title = t.Title,
                Description = t.Description,
                CourseId = t.CourseId,
                Lessons = t.Lessons.Select(x => new Lesson
                {
                    Title = x.Title,
                    ThemeId = x.ThemeId,
                    ArticleBody = x.ArticleBody,
                    Tests = x.Tests.Select(x => new Test
                    {
                        Title = x.Title,
                        LessonId = x.LessonId,
                        Questions = x.Questions.Select(x => new Question
                        {
                            Title = x.Title,
                            TestId = x.TestId,
                            Answers = x.Answers.Select(x => new Answer
                            {
                                Title = x.Title,
                                IsRight = x.IsRight,
                                QuestionId = x.QuestionId
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList()
        };

        var res = await _courseRepository.CreateAsync(course);

        return new CreateCourseResponseDto
        {
            Course = new CourseDto
            {
                Id = res.Id,
                Title = res.Title,
                Description = res.Description,
                //TeacherId = res.Teacher.Id,
                Themes = res.Themes.Select(t => new ThemeDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CourseId = t.CourseId,
                    Lessons = t.Lessons.Select(x => new LessonDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ThemeId = x.ThemeId,
                        ArticleBody = x.ArticleBody,
                        Tests = x.Tests.Select(x => new TestDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            LessonId = x.LessonId,
                            Questions = x.Questions.Select(x => new QuestionDto
                            {
                                Id = x.Id,
                                Title = x.Title,
                                TestId = x.TestId,
                                Answers = x.Answers.Select(x => new AnswerDto
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    IsRight = x.IsRight,
                                    QuestionId = x.QuestionId
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
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

    public async Task RemoveCourseForcedAsync(int id)
    {
        var result = await _courseRepository.DeleteAsync(await _courseRepository.GetByIdAsync(id));
        if (!result) throw new CourseNotFoundException("Course not found");
    }

    public async Task RemoveCourseAsync(int id, int userId)
    {
        var result = await _teacherRepository.GetCreatedCoursesAsync((await _teacherRepository.GetByUserIdAsync(userId)).Id);
        var candidate = result.FirstOrDefault(c => c.Id == id);
        if (candidate == null)
            throw new CourseNotFoundException("Course not found");

        await _courseRepository.DeleteAsync(candidate);
    }

    public async Task<UpdateCourseResponseDto> UpdateCourseAsync(UpdateCourseRequestDto request, int userId)
    {
        var result = await _courseRepository.GetByIdAsync(request.Course.Id);
        var own = (await _teacherRepository.GetCreatedCoursesAsync(result.Id))
            .FirstOrDefault(c => c.Teacher.Id == (_teacherRepository.GetByUserIdAsync(userId)).GetAwaiter().GetResult().Id);
        if (own is null)
            throw new CourseNotFoundException("Course not found");

        var res = await _courseRepository.UpdateAsync(new Course
        {
            Title = request.Course.Title,
            Description = request.Course.Description
        });
        return new UpdateCourseResponseDto
        {
            Course = new CourseDto
            {
                Id = res.Id,
                Title = res.Title,
                Description = res.Description,
                //TeacherId = res.Teacher.Id,
                Themes = res.Themes.Select(t => new ThemeDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CourseId = t.CourseId,
                    Lessons = t.Lessons.Select(x => new LessonDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ThemeId = x.ThemeId,
                        ArticleBody = x.ArticleBody,
                        Tests = x.Tests.Select(x => new TestDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            LessonId = x.LessonId,
                            Questions = x.Questions.Select(x => new QuestionDto
                            {
                                Id = x.Id,
                                Title = x.Title,
                                TestId = x.TestId,
                                Answers = x.Answers.Select(x => new AnswerDto
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    IsRight = x.IsRight,
                                    QuestionId = x.QuestionId
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
        
    }

    public async Task<UpdateCourseResponseDto> UpdateCourseForcedAsync(UpdateCourseRequestDto request)
    {
        var result = await _courseRepository.GetByIdAsync(request.Course.Id);
        if (result is null) throw new CourseNotFoundException("Course not found");

        var res = await _courseRepository.UpdateAsync(new Course
        {
            Title = request.Course.Title,
            Description = request.Course.Description,
            // Teacher = teacher,
            Themes = request.Course.Themes.Select(t => new Theme
            {
                Title = t.Title,
                Description = t.Description,
                CourseId = t.CourseId,
                Lessons = t.Lessons.Select(x => new Lesson
                {
                    Title = x.Title,
                    ThemeId = x.ThemeId,
                    ArticleBody = x.ArticleBody,
                    Tests = x.Tests.Select(x => new Test
                    {
                        Title = x.Title,
                        LessonId = x.LessonId,
                        Questions = x.Questions.Select(x => new Question
                        {
                            Title = x.Title,
                            TestId = x.TestId,
                            Answers = x.Answers.Select(x => new Answer
                            {
                                Title = x.Title,
                                IsRight = x.IsRight,
                                QuestionId = x.QuestionId
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList()
        });

        return new UpdateCourseResponseDto
        {
            Course = new CourseDto
            {
                Id = res.Id,
                Title = res.Title,
                Description = res.Description,
                //TeacherId = res.Teacher.Id,
                Themes = res.Themes.Select(t => new ThemeDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CourseId = t.CourseId,
                    Lessons = t.Lessons.Select(x => new LessonDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ThemeId = x.ThemeId,
                        ArticleBody = x.ArticleBody,
                        Tests = x.Tests.Select(x => new TestDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            LessonId = x.LessonId,
                            Questions = x.Questions.Select(x => new QuestionDto
                            {
                                Id = x.Id,
                                Title = x.Title,
                                TestId = x.TestId,
                                Answers = x.Answers.Select(x => new AnswerDto
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    IsRight = x.IsRight,
                                    QuestionId = x.QuestionId
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }
        };
    }
}