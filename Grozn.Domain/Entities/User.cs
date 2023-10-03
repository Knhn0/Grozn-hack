using System.ComponentModel.DataAnnotations;

namespace Grozn.Domain.Entities;

public class User : BaseEntity
{
    [Key] public int Id { get; set; }

    [Required] public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    [Required] public string Phone { get; set; } = string.Empty;
    
    public string? RefreshToken { get; set; }  
    
    public DateTime RefreshTokenExpiryTime { get; set; }
}