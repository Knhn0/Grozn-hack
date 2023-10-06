using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class LessonRepository : ILessonRepository
{
    private readonly Context _db;

    public LessonRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Lesson>> GetAllAsync()
    {
        return await _db.Lessons.ToListAsync();
    }

    public async Task<Lesson> GetByIdAsync(int id)
    {
        var result = await _db.Lessons.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new LessonNotFoundException("Lesson not found");
    }

    public async Task<Lesson> UpdateAsync(Lesson t)
    {
        var dbLesson = await _db.Lessons.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbLesson == null)
        {
            throw new LessonNotFoundException("Lesson not found");
        }

        dbLesson.Theme = t.Theme;
        dbLesson.Title = t.Title;
        dbLesson.ArticleBody = t.ArticleBody;
            
        await _db.SaveChangesAsync();
        return dbLesson;
    }

    public async Task<Lesson> CreateAsync(Lesson t)
    {
        var result = await _db.Lessons.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Lesson t)
    {
        var result = _db.Lessons.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }

    public async Task<List<Lesson>> GetLessonsByThemeIdAsync(int themeId)
    {
        var result = await _db.Lessons.Where(l => l.Theme.Id == themeId).ToListAsync();
        return result ?? throw new LessonNotFoundException("Lessons not found");
    }
}