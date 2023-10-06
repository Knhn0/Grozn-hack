using Contracts.Analitic;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ITestRepository _testRepository;
    private readonly ITestPercentRepository _testPercentRepository;

    public LessonService(ILessonRepository lessonRepository, ITestRepository testRepository, ITestPercentRepository testPercentRepository)
    {
        _lessonRepository = lessonRepository;
        _testRepository = testRepository;
        _testPercentRepository = testPercentRepository;
    }

    public async Task<ThemeResponseDto> GetAllTestsByThemeId(int themeId, int studentId)
    {
        var lessons = await _lessonRepository.GetLessonsByThemeIdAsync(themeId);
        var tests = new List<TestResponseDto>();

        for (int i = 0; i < lessons.Count; i++)
        {
            var thisTests = await _testRepository.GetAllTestsByLessonIdAsync(lessons[i].Id);

            for (int l = 0; l < thisTests.Count(); l++)
            {
                var res = await _testPercentRepository.GetTestPercentByTestIdAndStudentId(thisTests[l].Id, studentId);
                for (int m = 0; m < res.Count; m++)
                {
                    var ftest = new TestResponseDto();
                    ftest.TestBalls = Math.Pow(res[m].Percent * 100, 0);
                    ftest.LeftBalls = 100 - ftest.TestBalls - ftest.TestBalls;
                    
                    tests.Add(ftest);
                }
            }
        }

        var response = new ThemeResponseDto();
        response.Tests = tests;
        
        return response;
    }
}