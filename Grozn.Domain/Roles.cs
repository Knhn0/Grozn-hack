namespace Grozn.Domain;

public class Roles
{
    public const string Admin = "Admin";
    public const string Default = "Default";

    public static bool IsRoleValid(string role)
    {
        return typeof(Roles).GetFields().Any(f => f.Name == role);
    }
}