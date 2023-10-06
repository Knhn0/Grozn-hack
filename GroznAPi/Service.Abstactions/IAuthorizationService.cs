using Contracts.Autorization;
using Contracts.Registraton;

namespace Service.Abstactions;

public interface IAuthorizationService
{
    Task<RegistrationResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
}