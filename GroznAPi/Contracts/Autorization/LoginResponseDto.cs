namespace Contracts.Autorization;

public class LoginResponseDto
{
    public string Token { get; set; }
    public DateTime TokenExpiryTime { get; set; }
}