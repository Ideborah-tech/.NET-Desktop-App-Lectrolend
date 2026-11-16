using LectroLend.Core.Models;

namespace LectroLend.Core.Services;

// Small checks for what the user typed. Each one throws an AppException if the value is wrong.
public static class Check
{
    // The value must not be empty and must not be too long.
    public static void Required(string value, string fieldName, int maxLength)
    {
        if (IsEmpty(value))
            throw new AppException(fieldName + " is required.");
        if (value.Trim().Length > maxLength)
            throw new AppException(fieldName + " must be " + maxLength + " characters or fewer.");
    }

    // The value may be empty, but must not be too long.
    public static void MaxLength(string value, string fieldName, int maxLength)
    {
        if (!IsEmpty(value) && value.Trim().Length > maxLength)
            throw new AppException(fieldName + " must be " + maxLength + " characters or fewer.");
    }

    // A simple email check: something@something.something
    public static void Email(string value)
    {
        Required(value, "Email", 100);
        string email = value.Trim();
        int at = email.IndexOf('@');
        int lastDot = email.LastIndexOf('.');
        if (at < 1 || lastDot < at + 2 || lastDot == email.Length - 1 || email.Contains(' '))
            throw new AppException("Please enter a valid email address.");
    }

    // A password must have 8 to 72 characters (72 is the most BCrypt can use).
    public static void Password(string value)
    {
        if (value == null || value.Length < 8 || value.Length > 72)
            throw new AppException("The password must have 8 to 72 characters.");
    }

    public static bool IsEmpty(string value)
    {
        return value == null || value.Trim() == "";
    }

    // Removes spaces at the start and end. An empty value becomes null (saved as "nothing").
    public static string Clean(string value)
    {
        if (IsEmpty(value))
            return null;
        return value.Trim();
    }
}
