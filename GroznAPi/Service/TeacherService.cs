using System.ComponentModel.Design;
using Contracts.Teacher;
using Domain.Entities;
using Exceptions.Implementation;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    
    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }


    public async Task<GetAllTeachersDto> GetTeachers()
    {
        var list = await _teacherRepository.GetAllAsync();
        if (list == null) throw new Exception("List is empty");
        return new GetAllTeachersDto
        {
            AllTeachers = list
        };
    }

    public async Task<GetTeacherDto> GetTeacher(int id)
    {
        var resp = await _teacherRepository.GetByIdAsync(id);
        if (resp == null) throw new Exception("Teacher not found");
        return new GetTeacherDto
        {
            Courses = resp.Courses,
            Id = resp.Id,
            UserInfo = resp.UserInfo
        };
    }

    public async Task<UpdateTeacherResponseDto> UpdateTeacher(UpdateTeacherRequestDto req)
    {
        var teacher = await _teacherRepository.GetByIdAsync(req.Id);
        if (teacher == null) throw new Exception("Teacher not found");
        if (teacher.Id != 0) teacher.Id = req.Id;
        if (teacher.UserInfo != null) teacher.UserInfo = req.UserInfo;
        return new UpdateTeacherResponseDto
        {
            Id = teacher.Id,
            UserInfo = teacher.UserInfo,
            Courses = teacher.Courses
        };
    }

    public async Task<CreateTeacherResponseDto> CreateTeacher(CreateTeacherRequestDto req)
    {
        var teacher = new Teacher
        {
            Id = req.Id,
            UserInfo = req.UserInfo
        };
        var resp = await _teacherRepository.CreateAsync(teacher);
        return new CreateTeacherResponseDto
        {
            Id = teacher.Id,
            UserInfo = teacher.UserInfo,
            Courses = teacher.Courses,
        };
    }

    public async Task<bool> DeleteTeacher(DeleteTeacherRequestDto req)
    {
        var teacher = new Teacher
        {
            Id = req.Id,
            UserInfo = req.UserInfo,
            Courses = req.Courses
        };
        var resp = await _teacherRepository.DeleteAsync(teacher);
        return resp;
    }

    public async Task<GetTeacherInfoDto> GetUserInfo(int id)
    {
        var resp = await _teacherRepository.GetByIdAsync(id);
        return new GetTeacherInfoDto
        {
            UserInfo = resp.UserInfo
        };
    }


    public async Task<GetCreatedCourseDto> GetCreatedCourses(int id)
    {
        var resp = await _teacherRepository.GetByIdAsync(id);
        return new GetCreatedCourseDto
        {
            GetCourses = resp.Courses
        };
    }

    public async Task<bool> IsTeacher(int id)
    {
        var resp = await _teacherRepository.GetByIdAsync(id);
        var role = resp.UserInfo.Role.Title;
        return role == "Teacher";
    }
}