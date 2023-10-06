using System.ComponentModel.DataAnnotations;

namespace Contracts.Account;

public class AccountDto
{
    public int Id { get; set; }
    [EmailAddress]
    public string Username { get; set; }
    public UserInfoDto UserInfo { get; set; }
}