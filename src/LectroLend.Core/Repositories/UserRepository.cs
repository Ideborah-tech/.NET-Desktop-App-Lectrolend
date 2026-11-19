using LectroLend.Core.Data;
using LectroLend.Core.Models;

namespace LectroLend.Core.Repositories;

// REPOSITORY: the only place that reads and writes users in the database.
// The services ask the repository for data; they never write database queries themselves.
public class UserRepository
{
    private LectroLendDbContext db;

    public UserRepository(LectroLendDbContext db)
    {
        this.db = db;
    }

    // Finds a user by id. Returns null if there is none.
    public User FindById(long userId)
    {
        return db.Users.FirstOrDefault(u => u.UserId == userId);
    }

    // Finds a user by email (capital letters do not matter). Returns null if there is none.
    public User FindByEmail(string email)
    {
        string lowerEmail = email.Trim().ToLower();
        return db.Users.FirstOrDefault(u => u.Email.ToLower() == lowerEmail);
    }

    public bool EmailExists(string email)
    {
        return FindByEmail(email) != null;
    }

    public bool RegistrationNoExists(string registrationNo)
    {
        return db.Students.Any(s => s.RegistrationNo == registrationNo);
    }

    public bool LecturerStaffNoExists(string staffNo)
    {
        return db.Lecturers.Any(l => l.StaffNo == staffNo);
    }

    public bool AdminStaffNoExists(string staffNo)
    {
        return db.Admins.Any(a => a.StaffNo == staffNo);
    }

    // All users, sorted by role and then by name.
    public List<User> GetAll()
    {
        return db.Users.OrderBy(u => u.Role).ThenBy(u => u.FullName).ToList();
    }

    // Adds a new user. It is written to the database when Save() is called.
    public void Add(User user)
    {
        db.Users.Add(user);
    }

    // Writes all changes to the database.
    public void Save()
    {
        db.SaveChanges();
    }
}
