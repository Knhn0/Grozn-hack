using Contracts.Teacher;
using Domain.Entities;

namespace Service.Abstactions;

public interface ITeacherService
{
    Task<GetAllTeachersDto> GetTeachers();
    Task<GetTeacherDto> GetTeacher(int id);
    Task<UpdateTeacherResponseDto> UpdateTeacher(UpdateTeacherRequestDto req);
    Task<CreateTeacherResponseDto> CreateTeacher(CreateTeacherRequestDto req);
    Task<bool> DeleteTeacher(DeleteTeacherRequestDto req);
    Task<GetTeacherInfoDto> GetUserInfo(int id);
    Task<GetCreatedCourseDto> GetCreatedCourses(int id);
    Task<bool> IsTeacher(int id);


}