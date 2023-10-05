namespace Domain.Entities;

public class UserInfo
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string ThirdName { get; set; }
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
    
    public int AccountId { get; set; }
    public Account Account { get; set; }
}