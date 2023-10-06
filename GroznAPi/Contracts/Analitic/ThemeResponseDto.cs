using Newtonsoft.Json;

namespace Contracts.Analitic;

public class TestResponseDto
{
    public double TestBalls { get; set; }

    public double LeftBalls { get; set; }
}

public class ThemeResponseDto
{
    public List<TestResponseDto> Tests { get; set; }
}