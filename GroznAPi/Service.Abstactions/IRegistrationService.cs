using Contracts.Registraton;
using Domain.Entities;

namespace Service.Abstactions;

public interface IRegistrationService
{
    Task<RegistrationResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
}