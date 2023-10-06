using Contracts.Test;
using Domain.Entities;

namespace Service.Abstactions;

public interface ITestService
{
    Task<TestCreatedResponse> CreateTestAsync(CreateTestRequestDto request);
    Task<List<Test>> GetAllTests();
    Task<List<Test>> GetTestsByLesson();
    
}