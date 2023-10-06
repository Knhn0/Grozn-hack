using System.ComponentModel.DataAnnotations;
using Jwt;
using Newtonsoft.Json;

namespace Contracts.Autorization;

public class LoginRequestDto
{
    [JsonRequired]
    [MinLength(Constants.LogonNameLengthMinimum)]
    [MaxLength(Constants.LogonNameLengthMaximum)]
    [EmailAddress]
    public string UserName { get; set; }

    [JsonRequired]
    [MinLength(Constants.PasswordLengthMinimum)]
    [MaxLength(Constants.PasswordLengthMaximum)]
    public string Password { get; set; }

}