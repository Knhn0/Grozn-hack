using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class TestPercentRepository: ITestPercentRepository
{
    private readonly Context _db;

    public TestPercentRepository(Context db)
    {
        _db = db;   
    }

    public Task<List<StudentTestPercent>> GetTestPercentByTestIdAndStudentId(int testId, int studentId)
    {
        var testPercentTests = _db.StudentTestPercents.Where(t => t.Test.Id == testId && t.StudentId == studentId)
            .ToListAsync();
        return testPercentTests;
    }

}