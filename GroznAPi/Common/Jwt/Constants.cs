namespace Jwt;

/// <summary>
/// Constants used in the validation of Swagger strings etc.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Minimum length of a person login name.
    /// </summary>
    public const int LogonNameLengthMinimum = 3;

    /// <summary>
    /// Maximum length of a person login name.
    /// </summary>
    public const int LogonNameLengthMaximum = 50;

    /// <summary>
    /// Minimum length of a person's password.
    /// </summary>
    public const int PasswordLengthMinimum = 8;

    /// <summary>
    /// Maximum length of a person's password.
    /// </summary>
    public const int PasswordLengthMaximum = 40;

    public const int FirstNameLengthMinimum = 1;
    public const int FirstNameLengthMaximum = 50;

    public const int SecondNameLengthMinimum = 1;
    public const int SecondNameLengthMaximum = 50;
    
    public const int ThirdNameLengthMinimum = 1;
    public const int ThirdNameLengthMaximum = 50;
}