namespace Domain.Entities;

public class UserInfo
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string ThirdName { get; set; }
    
    public Role Role { get; set; } 
    public int AccountId { get; set; }
}