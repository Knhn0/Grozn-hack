namespace Contracts.Autorization;

public class LoginResponseDto
{
    public string Token { get; set; }
    public DateTime ExpirationTime { get; set; }
}