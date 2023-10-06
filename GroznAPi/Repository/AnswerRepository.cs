using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class AnswerRepository : IAnswerRepository
{
    private readonly Context _db;

    public AnswerRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Answer>> GetAllAsync()
    {
        return await _db.Answers.ToListAsync();
    }

    public async Task<Answer> GetByIdAsync(int id)
    {
        var result = await _db.Answers.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new AnswerNotFoundException("Answer not found");
    }

    public async Task<Answer> UpdateAsync(Answer t)
    {
        var dbAnswer = await _db.Answers.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbAnswer == null)
        {
            throw new AnswerNotFoundException("Answer not found");
        }

        dbAnswer.Question = t.Question;
        dbAnswer.Text = t.Text;
        dbAnswer.IsRight = t.IsRight;
            
        await _db.SaveChangesAsync();
        return dbAnswer;
    }

    public async Task<Answer> CreateAsync(Answer t)
    {
        var result = await _db.Answers.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Answer t)
    {
        var result = _db.Answers.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }
}