using System.ComponentModel.DataAnnotations;
using Jwt;
using Newtonsoft.Json;

namespace Contracts.Registraton;

public class RegistrationRequestDto
{
    [JsonRequired]
    [MinLength(Constants.LogonNameLengthMinimum)]
    [MaxLength(Constants.LogonNameLengthMaximum)]
    [EmailAddress]
    public string Username { get; set; }

    [JsonRequired]
    [MinLength(Constants.PasswordLengthMinimum)]
    [MaxLength(Constants.PasswordLengthMaximum)]
    public string Password { get; set; }

    [JsonRequired]
    [MinLength(Constants.FirstNameLengthMinimum)]
    [MaxLength(Constants.FirstNameLengthMaximum)]
    public string FirstName { get; set; }

    [JsonRequired]
    [MinLength(Constants.SecondNameLengthMinimum)]
    [MaxLength(Constants.SecondNameLengthMaximum)]
    public string SecondName { get; set; }

    [JsonRequired]
    [MinLength(Constants.ThirdNameLengthMinimum)]
    [MaxLength(Constants.ThirdNameLengthMaximum)]
    public string ThirdName { get; set; }

    [JsonRequired]
    public string Role { get; set; }
}