using LectroLend.Core.Data;
using LectroLend.Core.Models;
using LectroLend.Core.Repositories;

namespace LectroLend.Core.Services;

// SERVICE: the rules for signing in and creating accounts.
// It gets and saves data through the UserRepository.
public class AuthService
{
    private DbSettings settings;
    private UserService userService;

    public AuthService(DbSettings settings, UserService userService)
    {
        this.settings = settings;
        this.userService = userService;
    }

    // Returns the user if the email and password are correct.
    public User Login(string email, string password)
    {
        if (email == null) email = "";
        if (password == null) password = "";

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            UserRepository users = new UserRepository(db);
            User user = users.FindByEmail(email);

            // We give the same message for a wrong email and a wrong password (safer).
            if (user == null || !Passwords.Matches(password, user.PasswordHash))
                throw new AppException("Incorrect email or password.");

            if (!user.Active)
                throw new AppException("This account has been deactivated. Please contact the IT office.");

            return user;
        }
    }

    // Anyone can create a student or lecturer account. Only an admin can create admin accounts.
    public User Register(RegisterRequest request)
    {
        return userService.Create(request, false);
    }
}

// SERVICE: the rules for accounts and profiles.
// It gets and saves data through the UserRepository.
public class UserService
{
    private DbSettings settings;

    public UserService(DbSettings settings)
    {
        this.settings = settings;
    }

    // Creates a new account. Set allowAdmin to true only when an admin is creating the account.
    public User Create(RegisterRequest request, bool allowAdmin)
    {
        // 1. Check what was typed.
        Check.Required(request.FullName, "Full name", 100);
        Check.Email(request.Email);
        Check.Password(request.Password);
        Check.MaxLength(request.Phone, "Phone", 20);
        if (request.Role == Role.ADMIN && !allowAdmin)
            throw new AppException("Only an administrator can create administrator accounts.");

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            UserRepository users = new UserRepository(db);

            // 2. The email must not be used already.
            if (users.EmailExists(request.Email))
                throw new AppException("An account with this email already exists.");

            // 3. Make the right kind of user.
            User user;
            if (request.Role == Role.STUDENT)
            {
                Check.Required(request.RegistrationNo, "Registration number", 20);
                string number = request.RegistrationNo.Trim();
                if (users.RegistrationNoExists(number))
                    throw new AppException("This registration number is already used.");

                Student student = new Student();
                student.RegistrationNo = number;
                student.Faculty = Check.Clean(request.Faculty);
                student.Program = Check.Clean(request.Program);
                user = student;
            }
            else if (request.Role == Role.LECTURER)
            {
                Check.Required(request.StaffNo, "Staff number", 20);
                string number = request.StaffNo.Trim();
                if (users.LecturerStaffNoExists(number))
                    throw new AppException("This staff number is already used.");

                Lecturer lecturer = new Lecturer();
                lecturer.StaffNo = number;
                lecturer.Department = Check.Clean(request.Department);
                user = lecturer;
            }
            else
            {
                Check.Required(request.StaffNo, "Staff number", 20);
                string number = request.StaffNo.Trim();
                if (users.AdminStaffNoExists(number))
                    throw new AppException("This staff number is already used.");

                Admin admin = new Admin();
                admin.StaffNo = number;
                user = admin;
            }

            // 4. Fill in the common details and save.
            user.FullName = request.FullName.Trim();
            user.Email = request.Email.Trim().ToLower();
            user.Phone = Check.Clean(request.Phone);
            user.PasswordHash = Passwords.Hash(request.Password);

            users.Add(user);
            users.Save();
            return user;
        }
    }

    // All users, sorted by role and then by name.
    public List<User> List()
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            UserRepository users = new UserRepository(db);
            return users.GetAll();
        }
    }

    public User Get(long userId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            UserRepository users = new UserRepository(db);
            return FindUser(users, userId);
        }
    }

    // Turns an account on (active = true) or off (active = false).
    public void SetActive(long userId, bool active, long adminId)
    {
        if (userId == adminId && !active)
            throw new AppException("You cannot deactivate your own account.");

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            UserRepository users = new UserRepository(db);
            User user = FindUser(users, userId);
            user.Active = active;
            users.Save();
        }
    }

    // Saves the details a user may change about themselves.
    public User UpdateProfile(long userId, string fullName, string phone, string faculty, string program, string department)
    {
        Check.Required(fullName, "Full name", 100);
        Check.MaxLength(phone, "Phone", 20);

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            UserRepository users = new UserRepository(db);
            User user = FindUser(users, userId);
            user.FullName = fullName.Trim();
            user.Phone = Check.Clean(phone);

            if (user is Student)
            {
                Student student = (Student)user;
                student.Faculty = Check.Clean(faculty);
                student.Program = Check.Clean(program);
            }
            if (user is Lecturer)
            {
                Lecturer lecturer = (Lecturer)user;
                lecturer.Department = Check.Clean(department);
            }

            users.Save();
            return user;
        }
    }

    public void ChangePassword(long userId, string currentPassword, string newPassword)
    {
        Check.Password(newPassword);

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            UserRepository users = new UserRepository(db);
            User user = FindUser(users, userId);

            if (!Passwords.Matches(currentPassword, user.PasswordHash))
                throw new AppException("Your current password is incorrect.");
            if (Passwords.Matches(newPassword, user.PasswordHash))
                throw new AppException("Please choose a new password that is different from the current one.");

            user.PasswordHash = Passwords.Hash(newPassword);
            users.Save();
        }
    }

    // Asks the repository for the user, and stops with a message if there is none.
    private static User FindUser(UserRepository users, long userId)
    {
        User user = users.FindById(userId);
        if (user == null)
            throw new AppException("User not found.");
        return user;
    }
}
