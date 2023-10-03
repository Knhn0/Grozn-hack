using System.ComponentModel.DataAnnotations;

namespace Grozn.Services.Contracts;

public class AutorizationRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    [EmailAddress] public string? Email { get; set; }
}