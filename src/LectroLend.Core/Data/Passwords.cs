namespace LectroLend.Core.Data;

// Passwords are never saved as they are typed.
// We save a "hash": a scrambled version that cannot be turned back into the password.
// To check a password, we scramble what the user typed and compare.
public static class Passwords
{
    // Turns a password into a hash, for saving.
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 10);
    }

    // Returns true if the typed password matches the saved hash.
    public static bool Matches(string password, string savedHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, savedHash);
        }
        catch (Exception)
        {
            return false; // the saved hash is not valid
        }
    }
}
