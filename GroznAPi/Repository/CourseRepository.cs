using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class CourseRepository : ICourseRepository
{
    private readonly Context _db;

    public CourseRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await _db.Courses.ToListAsync();
    }

    public async Task<Course> GetByIdAsync(int id)
    {
        var result = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new Exception("User not found");
    }

    public async Task<Course> AddStudent(int id, Student student)
    {
        var dbCourse = await GetByIdAsync(id);
        dbCourse.Students.Add(student);
        await _db.SaveChangesAsync();
        return dbCourse;
    }

    public async Task<Course> UpdateAsync(Course t)
    {
        var dbCourse = await _db.Courses.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbCourse == null)
        {
            throw new Exception("Course not found");
        }

        dbCourse.Description = t.Description;
        dbCourse.Title = t.Description;
        dbCourse.Students = t.Students;
        dbCourse.Teacher = t.Teacher;
        dbCourse.Themes = t.Themes;
            
        await _db.SaveChangesAsync();
        return dbCourse;
    }

    public async Task<Course> CreateAsync(Course t)
    {
        var result = await _db.Courses.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Course t)
    {
        var result = _db.Courses.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }
}