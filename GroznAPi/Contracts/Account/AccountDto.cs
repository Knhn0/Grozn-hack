namespace Contracts.Account;

public class AccountDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public UserInfoDto UserInfo { get; set; }
}