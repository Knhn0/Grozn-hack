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

    /// <summary>
    /// Maximum length of a customer's name.
    /// </summary>
    public const int CustomerNameLengthMaximum = 100;

    /// <summary>
    /// Maximum number of customers returned by a name search
    /// </summary>
    public const int CustomerSearchMaximumRowsToReturn = 100;

    /// <summary>
    /// Maximum length of StockItem name
    /// </summary>
    public const int StockItemNameMaximumLength = 100;

    /// <summary>
    /// Maximum number of customers returned by a name search
    /// </summary>
    public const int StockItemSearchMaximumRowsToReturn = 100;

}