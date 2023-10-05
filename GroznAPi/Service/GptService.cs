using System.Text;
using Contracts.Gpt;
using Newtonsoft.Json;
using Presistence;

namespace Service;

public class GptService
{
    private readonly HttpClient _httpClient;
    
    public GptService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://91.220.109.216:1337/api/v1/");
    }

    public async Task<GptResponseMessageDto> GenMessage(string reqText)
    {
        var jsonResponse = await _httpClient.GetAsync($"message/{reqText}");
        return JsonConvert.DeserializeObject<GptResponseMessageDto>(await jsonResponse.Content.ReadAsStringAsync());
    }

    public async Task<GptResponseQuestionDto> GenQuestion(GptQuestionRequestDto req)
    {
        var jsonResponse = await _httpClient.PostAsync("message/question/", new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json"));
        return JsonConvert.DeserializeObject<GptResponseQuestionDto>(await jsonResponse.Content.ReadAsStringAsync());
    }
    
}