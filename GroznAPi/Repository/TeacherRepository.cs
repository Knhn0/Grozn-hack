using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class TeacherRepository : ITeacherRepository
{
    private readonly Context _db;

    public TeacherRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Teacher>> GetAllAsync()
    {
        return await _db.Teachers.ToListAsync();
    }

    public async Task<Teacher> GetByIdAsync(int id)
    {
        var result = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new TeacherNotFoundException("Teacher not found");
    }

    public async Task<Teacher> UpdateAsync(Teacher t)
    {
        var dbTeacher = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbTeacher == null)
        {
            throw new TeacherNotFoundException("Teacher not found");
        }

        dbTeacher.Courses = t.Courses;
        dbTeacher.UserInfo = t.UserInfo;
            
        await _db.SaveChangesAsync();
        return dbTeacher;
    }

    public async Task<Teacher> CreateAsync(Teacher t)
    {
        var result = await _db.Teachers.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Teacher t)
    {
        var result = _db.Teachers.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }

    public async Task<UserInfo> GetUserInfoAsync(int id)
    {
        var result = await GetByIdAsync(id);
        return result.UserInfo ?? throw new UserInfoNotFoundException("UserInfo not found");
    }

    public async Task<Teacher> GetByUserIdAsync(int userId)
    {
        var result = await _db.Teachers.FirstOrDefaultAsync(student => student.UserInfo.Id == userId);
        return result ?? throw new TeacherNotFoundException("UserInfo not found");
    }

    public async Task<List<Course>> GetCreatedCoursesAsync(int id)
    {
        var result = _db.Courses.Where(course => course.Teacher == GetByIdAsync(id).GetAwaiter().GetResult());
        return await result.ToListAsync();
    }
    
    public async Task<bool> IsTeacherAsync(int userId)
    {
        try
        {
            await GetByUserIdAsync(userId);
            return true;
        }
        catch (TeacherNotFoundException)
        {
            return false;
        }
    }
}