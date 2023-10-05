using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class StudentRepository : IStudentRepository
{
    private readonly Context _db;

    public StudentRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _db.Students.ToListAsync();
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        var result = await _db.Students.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new StudentNotFoundException("Course not found");
    }

    public async Task<Student> UpdateAsync(Student t)
    {
        var dbStudent = await _db.Students.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbStudent == null)
        {
            throw new CourseNotFoundException("Student not found");
        }

        dbStudent.Courses = t.Courses;
        dbStudent.StudentTestPercents = t.StudentTestPercents;
        dbStudent.User = t.User;
            
        await _db.SaveChangesAsync();
        return dbStudent;
    }

    public async Task<Student> CreateAsync(Student t)
    {
        var result = await _db.Students.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Student t)
    {
        var result = _db.Students.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }

    public async Task<UserInfo> GetUserInfoAsync(int studentId)
    {
        var result = await GetByIdAsync(studentId);
        return result.User ?? throw new UserInfoNotFoundException("UserInfo not found");
    }

    public async Task<Student> GetByUserIdAsync(int userId)
    {
        var result = await _db.Students.FirstOrDefaultAsync(student => student.User.Id == userId);
        return result ?? throw new UserInfoNotFoundException("UserInfo not found");
    }
}