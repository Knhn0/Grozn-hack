using Contracts.Aws;
using Contracts.Test;
using Domain.Entities;

namespace Service.Abstactions;

public interface ITestService
{
    Task<CreateTestResponseDto> CreateTestAsync(CreateTestRequestDto request);
    Task<List<TestDto>> GetAllTestsAsync();
    Task<List<TestDto>> GetTestsByLessonAsync(int lessonId);
    Task<UpdateTestResponseDto> UpdateTestAsync(UpdateTestRequestDto testDto);
    Task<bool> RemoveTestAsync(int id);
    Task<AwsFileDto> SetQuestionResourceAsync(int testId, int questionId, Resource resource);
}