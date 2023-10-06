using Domain.Entities;

namespace Repository.Abstractions;

public interface ITestPercentRepository
{
    Task<List<StudentTestPercent>> GetTestPercentByTestIdAndStudentId(int testId, int studentId);

    Task<List<StudentTestPercent>> GetTestPercentsByLessonId(int lessonId, int studentId);
}