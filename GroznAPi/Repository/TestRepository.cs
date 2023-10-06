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

    public async Task<List<Question>> CreateQuestionsAsync(int id, params Question[] questions)
    {
        var test = await GetByIdAsync(id);
        foreach (var question in questions)
        {
            question.Test = test;
            await _db.Questions.AddAsync(question);
        }

        return await GetQuestionsAsync(id);
    }

    public async Task<Course> GetCourseAsync(int testId)
    {
        var list = await _db.Courses.FromSqlRaw(@"SELECT ""Id"" FROM ""Courses"" c
    JOIN ""Themes"" t ON c.""Id"" = t.""CourseId""
    JOIN ""Lessons"" l on t.""Id"" = l.""ThemeId""
    JOIN ""Tests"" tt on tt.""LessonId"" = l.""Id""
    WHERE tt.""Id"" = {0};", testId).ToListAsync();
        
        var id = list.FirstOrDefault().Id;
        
        return await _db.Courses.FirstOrDefaultAsync(id);
    }

    public async Task<List<Question>> GetQuestionsAsync(int id)
    {
        return await _db.Questions.Where(q => q.Test.Id == id).ToListAsync();
    }
}