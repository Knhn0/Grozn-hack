using Domain.Entities;

namespace Repository.Abstractions;

public interface ITestPercentRepository
{
    public Task<List<StudentTestPercent>> GetTestPercentByTestIdAndStudentId(int testId, int studentId);
}