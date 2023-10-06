using Contracts.Test;

namespace Service.Abstactions;

public interface ITestService
{
    Task<CreateTestResponseDto> CreateTestAsync(CreateTestRequestDto request);
    Task<List<TestDto>> GetAllTestsAsync();
    Task<List<TestDto>> GetTestsByLessonAsync(int lessonId);
    Task<TestDto> EditTestAsync(TestDto testDto);
    Task<TestDto> RemoveTestAsync(TestDto testDto);
}