﻿using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class ThemeRepository : IThemeRepository
{
    private readonly Context _db;

    public ThemeRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Lesson>> GetLessonsAsync()
    {
        return await _db.Lessons.ToListAsync();
    }

    public async Task<List<Theme>> GetAllAsync()
    {
        return await _db.Themes.ToListAsync();
    }

    public async Task<Theme?> GetByIdAsync(int id)
    {
        var res = await _db.Themes
            .Include(x => x.Lessons.Select(x => x.Tests.Select(x => x.Questions.Select(x => x.Answers))))
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }

    public async Task<Theme> UpdateAsync(Theme t)
    {
        var theme = await _db.Themes.Include(x => x.Lessons.Select(x => x.Tests.Select(x => x.Questions.Select(x => x.Answers)))).FirstOrDefaultAsync(x => x.Id == t.Id);
        if (theme == null) throw new Exception("Theme not found");
        theme.Title = t.Title;
        theme.Description = t.Description;
        await _db.SaveChangesAsync();
        return theme;
    }

    public async Task<Theme> CreateAsync(Theme t)
    {
        var res = await _db.Themes.AddAsync(t);
        await _db.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<bool> DeleteAsync(int id )
    {
        var res = _db.Themes.Remove(new Theme(){Id = id});
        await _db.SaveChangesAsync();
        return res.State == EntityState.Deleted;
    }

    public async Task<List<Theme>> GetByCourseId(int courseId)
    {
        var res = await _db.Themes.FromSqlRaw(@"select * from ""Themes"" where ""CourseId"" = {0};", courseId).ToListAsync();
        if (res.Count == 0) throw new ThemeNotFoundException("Theme not found");
        return res;
    }
}