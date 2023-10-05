using System.ComponentModel.DataAnnotations;
using Jwt;
using Newtonsoft.Json;

namespace Contracts.Registraton;

public class RegistrationRequestDto
{
    [JsonRequired]
    [MinLength(Constants.LogonNameLengthMinimum)]
    [MaxLength(Constants.LogonNameLengthMaximum)]
    public string Login { get; set; }

    [JsonRequired]
    [MinLength(Constants.PasswordLengthMinimum)]
    [MaxLength(Constants.PasswordLengthMaximum)]
    public string Password { get; set; }
}