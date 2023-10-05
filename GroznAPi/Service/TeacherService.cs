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
}