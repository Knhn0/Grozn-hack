namespace Contracts.Account;

public class UserInfoDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string ThirdName { get; set; }

    public RoleDto Role { get; set; }
}
