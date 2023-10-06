using Domain.Entities;
using Exceptions.Implementation;
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
        return result ?? throw new CourseNotFoundException("Course not found");
    }

    public async Task<Course> AddStudent(int id, Student student)
    {
        var dbCourse = await GetByIdAsync(id);
        dbCourse.Students.Add(student);
        await _db.SaveChangesAsync();
        return dbCourse;
    }

    public async Task<ICollection<Theme>> GetThemesById(int id)
    {
        var dbCourse = await GetByIdAsync(id);
        return dbCourse == null ? throw new CourseNotFoundException("Course not found") : dbCourse.Themes;
    }

    public async Task<Course> UpdateAsync(Course t)
    {
        var dbCourse = await _db.Courses.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbCourse == null)
        {
            throw new CourseNotFoundException("Course not found");
        }

        if (!String.IsNullOrEmpty(t.Description)) dbCourse.Description = t.Description;
        if (!String.IsNullOrEmpty(t.Title)) dbCourse.Title = t.Title;
        if (t.Students is not null) dbCourse.Students = t.Students;
        if (t.Teacher is not null) dbCourse.Teacher = t.Teacher;
        if (t.Themes is not null) dbCourse.Themes = t.Themes;
            
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