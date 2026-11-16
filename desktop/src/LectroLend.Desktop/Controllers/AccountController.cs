using LectroLend.Core.Models;
using LectroLend.Core.Services;

namespace LectroLend.Desktop.Controllers;

// Controller for everything about accounts:
// signing in and out, creating accounts, the profile, and turning accounts on or off.
public class AccountController
{
    private AuthService authService;
    private UserService userService;

    public AccountController(AuthService authService, UserService userService)
    {
        this.authService = authService;
        this.userService = userService;
    }

    // ---- Signing in and out ----

    // Checks the email and password. If they are right, this person is now signed in.
    public void SignIn(string email, string password)
    {
        Session.CurrentUser = authService.Login(email, password);
    }

    public void SignOut()
    {
        Session.CurrentUser = null;
    }

    // ---- Creating accounts ----

    // Creates an account from what was typed in the "Create account" window.
    // byAdmin = true when the IT office creates it (then it may also be an administrator).
    // When a student or lecturer registers themselves, they are signed in straight away.
    public void CreateAccount(Role role, string fullName, string email, string phone, string password,
                              string number, string faculty, string program, string department, bool byAdmin)
    {
        RegisterRequest request = new RegisterRequest();
        request.Role = role;
        request.FullName = fullName;
        request.Email = email;
        request.Phone = phone;
        request.Password = password;
        request.Faculty = faculty;
        request.Program = program;
        request.Department = department;

        // Students have a registration number. Lecturers and admins have a staff number.
        if (role == Role.STUDENT)
            request.RegistrationNo = number;
        else
            request.StaffNo = number;

        if (byAdmin)
        {
            userService.Create(request, true);
        }
        else
        {
            User newUser = authService.Register(request);
            Session.CurrentUser = newUser; // sign the new person in
        }
    }

    // ---- The signed-in person's profile ----

    public User GetMyProfile()
    {
        return userService.Get(Session.CurrentUser.UserId);
    }

    public void UpdateMyProfile(string fullName, string phone, string faculty, string program, string department)
    {
        Session.CurrentUser = userService.UpdateProfile(Session.CurrentUser.UserId, fullName, phone, faculty, program, department);
    }

    public void ChangeMyPassword(string currentPassword, string newPassword, string repeatPassword)
    {
        if (newPassword != repeatPassword)
            throw new AppException("The two new passwords are not the same.");

        userService.ChangePassword(Session.CurrentUser.UserId, currentPassword, newPassword);
    }

    // How many bookings the signed-in person may hold at once (3 for students, 5 for lecturers).
    public int MyBookingLimit()
    {
        if (Session.CurrentUser is IBorrower)
        {
            IBorrower borrower = (IBorrower)Session.CurrentUser;
            return borrower.MaxActiveReservations;
        }
        return 0; // admins do not borrow
    }

    // Text for the top bar, for example "Ingabire Deborah  (Student)".
    public string MyNameAndRole()
    {
        return Session.CurrentUser.FullName + "  (" + Labels.Of(Session.CurrentUser.Role) + ")";
    }

    // ---- Managing all accounts (IT office) ----

    public List<User> GetAllUsers()
    {
        return userService.List();
    }

    // Turns an account on (active = true) or off (active = false).
    public void SetActive(User user, bool active)
    {
        userService.SetActive(user.UserId, active, Session.CurrentUser.UserId);
    }
}
