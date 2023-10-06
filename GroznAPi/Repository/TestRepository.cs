using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class TestRepository : ITestRepository
{
    private readonly Context _db;

    public TestRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Test>> GetAllAsync()
    {
        return await _db.Tests.ToListAsync();
    }

    public async Task<Test> GetByIdAsync(int id)
    {
        var result = await _db.Tests.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new ResourceNotFoundException("Test not found");
    }

    public async Task<Test> UpdateAsync(Test t)
    {
        var dbTest = await _db.Tests.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbTest == null)
        {
            throw new TestNotFoundException("Test not found");
        }

        dbTest.Title = t.Title;
        dbTest.Description = t.Description;
        dbTest.Lesson = t.Lesson;
            
        await _db.SaveChangesAsync();
        return dbTest;
    }

    public async Task<Test> CreateAsync(Test t)
    {
        var result = await _db.Tests.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Test t)
    {
        var result = _db.Tests.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }

    public async Task<List<Test>> GetAllTestsByLessonIdAsync(int lessonId)
    {
        var result =  await _db.Tests.Where(t => t.Lesson.Id == lessonId).ToListAsync();
        return result ?? throw new TestNotFoundException("Tests not found");
    }
}