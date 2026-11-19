using LectroLend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LectroLend.Core.Data;

// The connection between our C# classes and the PostgreSQL tables (Entity Framework Core).
// Each "DbSet" is one table. For example, db.Rooms is the "room" table.
//
// How to use it:
//     using (LectroLendDbContext db = new LectroLendDbContext(settings))
//     {
//         List<Room> rooms = db.Rooms.ToList();   // read all rooms
//     }
public class LectroLendDbContext : DbContext
{
    private DbSettings settings;

    public LectroLendDbContext(DbSettings settings)
    {
        this.settings = settings;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReturnRecord> ReturnRecords { get; set; }

    // Tells Entity Framework which database to use.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(settings.ConnectionString());
    }

    // Extra rules that the [Table] and [Column] lines in the model classes cannot say.
    protected override void OnModelCreating(ModelBuilder model)
    {
        // Save enum values as text (for example "AVAILABLE") instead of numbers.
        model.Entity<User>().Property(u => u.Role).HasConversion<string>();
        model.Entity<Equipment>().Property(e => e.Type).HasConversion<string>();
        model.Entity<Equipment>().Property(e => e.Status).HasConversion<string>();
        model.Entity<Reservation>().Property(r => r.Status).HasConversion<string>();

        // Each reservation has at most one return record.
        model.Entity<Reservation>()
            .HasOne(r => r.ReturnRecord)
            .WithOne(rr => rr.Reservation)
            .HasForeignKey<ReturnRecord>(rr => rr.ReservationId);
    }
}
