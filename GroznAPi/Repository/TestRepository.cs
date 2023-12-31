﻿using Domain.Entities;
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
        return await _db.Tests.Include(x => x.Questions).Include(x => x.Questions.Select(x => x.Answers)).ToListAsync();
    }

    public async Task<Test> GetByIdAsync(int id)
    {
        var result = await _db.Tests.Include(x => x.Questions).Include(x => x.Questions.Select(x => x.Answers)).FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new ResourceNotFoundException("Test not found");
    }

    public async Task<Test> UpdateAsync(Test t)
    {
        var dbTest = await _db.Tests.Include(x => x.Questions).Include(x => x.Questions.Select(x => x.Answers)).FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbTest == null)
        {
            throw new TestNotFoundException("Test not found");
        }

        dbTest.Title = t.Title;
        dbTest.Description = t.Description;
        dbTest.LessonId = t.LessonId;
        dbTest.Questions = t.Questions;

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
        var result = await _db.Tests.Include(x => x.Questions).Include(x => x.Questions.Select(x => x.Answers)).Where(t => t.LessonId == lessonId).ToListAsync();
        return result ?? throw new TestNotFoundException("Tests not found");
    }

    public async Task<List<Question>> CreateQuestionsAsync(int id, params Question[] questions)
    {
        var test = await GetByIdAsync(id);
        foreach (var question in questions)
        {
            question.TestId = test.Id;
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

        var candidate = await _db.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (candidate is null) throw new CourseNotFoundException("Course not found");
        return candidate;
    }

    public async Task<List<Question>> GetQuestionsAsync(int id)
    {
        return await _db.Questions.Include(x=> x.Answers).Where(q => q.TestId == id).ToListAsync();
    }

    public async Task<Question> UpdateQuestionAsync(Question question)
    {
        var dbQuestion = await _db.Questions.FirstOrDefaultAsync(q => q.Id == question.Id);
        if (dbQuestion is null) throw new QuestionNotFoundException("Question not found");
        
        dbQuestion.Title = question.Title;
        dbQuestion.Resource = question.Resource;
        
        await _db.SaveChangesAsync();
        return dbQuestion;
    }
}