using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectroLend.Core.Models;

// The [Table] and [Column] lines tell the database which table and column each value is saved in.

// Every person who uses the app. It is "abstract": we only create Students, Lecturers and Admins.
[Table("users")]
public abstract class User
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("full_name")]
    public string FullName { get; set; }

    [Column("email")]
    public string Email { get; set; }

    // We never save the real password, only a scrambled version of it (a "hash").
    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    // A deactivated account can no longer sign in.
    [Column("is_active")]
    public bool Active { get; set; } = true;

    [Column("role")]
    public Role Role { get; set; }
}

// Students and lecturers can borrow equipment. Each one has a limit on how many bookings they can hold.
public interface IBorrower
{
    int MaxActiveReservations { get; }
}

[Table("student")]
public class Student : User, IBorrower
{
    public Student()
    {
        Role = Role.STUDENT;
    }

    [Column("registration_no")]
    public string RegistrationNo { get; set; }

    [Column("faculty")]
    public string Faculty { get; set; }

    [Column("program")]
    public string Program { get; set; }

    // A student can hold up to 3 bookings at the same time.
    public int MaxActiveReservations
    {
        get { return 3; }
    }
}

[Table("lecturer")]
public class Lecturer : User, IBorrower
{
    public Lecturer()
    {
        Role = Role.LECTURER;
    }

    [Column("staff_no")]
    public string StaffNo { get; set; }

    [Column("department")]
    public string Department { get; set; }

    // A lecturer can hold up to 5 bookings at the same time.
    public int MaxActiveReservations
    {
        get { return 5; }
    }
}

// The IT administrator. Admins manage the equipment but cannot borrow.
[Table("admin")]
public class Admin : User
{
    public Admin()
    {
        Role = Role.ADMIN;
    }

    [Column("staff_no")]
    public string StaffNo { get; set; }
}
