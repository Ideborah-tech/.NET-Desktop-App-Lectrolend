using LectroLend.Core.Models;

namespace LectroLend.Desktop.Controllers;

// Remembers who is signed in, while the app is open.
// The AccountController sets it when someone signs in, and clears it when they sign out.
public static class Session
{
    // The person who is signed in right now (null when nobody is signed in).
    public static User CurrentUser;

    // True if the signed-in person is the IT administrator.
    public static bool IsAdmin()
    {
        return CurrentUser != null && CurrentUser.Role == Role.ADMIN;
    }
}
