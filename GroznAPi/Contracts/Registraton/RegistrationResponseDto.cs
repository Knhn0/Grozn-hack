using Contracts.Account;

namespace Contracts.Registraton;

public class RegistrationResponseDto
{
    public AccountDto Account { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiryTime { get; set; }
}