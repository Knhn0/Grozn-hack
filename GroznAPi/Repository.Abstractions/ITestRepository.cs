using Domain.Entities;

namespace Repository.Abstractions;

public interface ITestRepository
{
    Task<List<Test>> GetAllAsync();
    Task<Test> GetByIdAsync(int id);
    Task<Test> UpdateAsync(Test t);
    Task<Test> CreateAsync(Test t);
    Task<bool> DeleteAsync(Test t);
    Task<List<Test>> GetAllTestsByLessonIdAsync(int lessonId);
    Task<List<Question>> GetQuestionsAsync(int id);
    Task<List<Question>> CreateQuestionsAsync(int id, params Question[] questions);
    Task<Course> GetCourseAsync(int id);
    Task<Question> UpdateQuestionAsync(Question question);
}