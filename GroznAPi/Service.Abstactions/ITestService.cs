using Contracts.Test;
using Domain.Entities;

namespace Service.Abstactions;

public interface ITestService
{
    Task<TestCreatedResponse> CreateTestAsync(CreateTestRequestDto request);
    Task<List<Test>> GetAllTestsAsync();
    Task<List<Test>> GetTestsByLessonAsync(int lessonId);
    Task<Test> GetTestByIdAsync(int id);
}